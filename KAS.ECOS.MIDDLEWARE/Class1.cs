using KAS.ECOS.MIDDLEWARE.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http;
using System.Text;

namespace KAS.ECOS.MIDDLEWARE
{
    public class Middleware
    {
        bool isDebug = false;
        private readonly RequestDelegate next;

        public Middleware(RequestDelegate _next, bool isDebug)
        {
            next = _next;
            this.isDebug = isDebug;
        }

        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {


            string content = "";
            var stream = context.Request.Body;
            // Optimization: don't buffer the request if
            // there was no stream or if it is rewindable.
            if (stream == Stream.Null || stream.CanSeek)
            {
                await next(context);
                return;

            }
            else
            {
                try
                {
                    using (var buffer = new MemoryStream())
                    {
                        // Copy the request stream to the memory stream.
                        await stream.CopyToAsync(buffer);

                        // Rewind the memory stream.
                        buffer.Position = 0L;

                        content = System.Text.Encoding.UTF8.GetString(buffer.ToArray());
                        // Replace the request stream by the memory stream.
                        //   context.Request.Body = buffer;

                        // Invoke the rest of the pipeline.
                        //   await next(context);
                    }
                }
                catch { }
            }

            string localPath = context.Request.Path.ToString().ToLower();


            string kasProduct = (context.Request.Headers["Product"].ToString() ?? "WEB").ToUpper();

            //context.Request.EnableBuffering();
            // var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            //  await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            //  context.Request.Body.Position = 0;




            string version = context.Request.Headers["Version"];
            string token = context.Request.Headers["token"];
            string sign = context.Request.Headers["sign"];
            string X = context.Request.Headers["X"];
            long localTime = Convert.ToInt64(context.Request.Headers["time"]);
            string location = context.Request.Headers["location"];
            string deviceInfo = context.Request.Headers["deviceinfo"];
            version = version ?? "1000";
            string customerId = context.Request.Query["customerid"];

            var newObject = new InEntity()
            {

                clientTimeGMT = localTime,
                data = content,
                DeivceInfo = deviceInfo ?? "",
                KASProductName = kasProduct ?? "",
                Location = location ?? "",
                Sign = sign ?? "",
                Token = token ?? "",
                IP = "",
                isNeedEncrypt = (X ?? "0") == "1",
                Version = Convert.ToInt32(version.Replace(".", ""))
            };
            if (!String.IsNullOrEmpty(customerId))
            {
                newObject.Postgre_HOS_Connection = $"Host=127.0.0.1;Database=KAS.HOS.{customerId.ToUpper()};Username=kashos;Password=kas3.14159";
                newObject.Postgre_SYNC_Connection = $"Host=127.0.0.1;Database=KAS.SYNC;Username=kashos;Password=kas3.14159";
            }

            try
            {
                newObject.IP = context.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                newObject.IP = "";
            }


            string sToken = "";
            byte xToken = 0;
            switch (kasProduct.ToUpper())
            {
                case "ECOS":
                    xToken = 81;
                    sToken = "e4a2d4dafac7227ebe69e6972cfe22c5";
                    break;
                case "HOS":
                    xToken = 71;
                    sToken = "d0bb11aea7b6258e23a3371c763a3eed";
                    break;
                case "MOS":
                    xToken = 21;
                    sToken = "921208b14f34d17ebdd6c771da883a84";
                    break;
                case "POS":
                    xToken = 31;
                    sToken = "c92da0de253abca837c881ee704dff93";
                    break;
                case "CAP":
                    xToken = 41;
                    sToken = "ec10e61d3c3ebb7951423cac08dfb3cc";
                    break;
                case "OCC":
                    xToken = 0;
                    sToken = "5db75df8578fd480db2fd5cd643f214b";
                    break;
                case "WEB":
                    xToken = 0;
                    sToken = "76fdb9f147230fc3b6ea909ff1aa6881";
                    break;
                case "VNPLACE":
                    xToken = 51;
                    sToken = "97618bab28a3538465357aea5bd43935";
                    break;
                case "DP":
                    xToken = 61;
                    sToken = "aff48fa51d7b0dabc3c53ae363de450d";
                    break;
                default:
                    if (!isDebug)
                    {
                        context.Response.StatusCode = 404; //Truy cập vào API của KAS nhưng không có tên sẽ bị từ chối và báo lỗi 404
                        return;
                    }
                    break;

            }


            if (newObject.isNeedEncrypt)
            {
                string tmpContent = Decrypt_Decompress(newObject.data, xToken);
                if (string.IsNullOrEmpty(tmpContent)) //Không giải mã được, trả về lỗi 
                {
                    context.Response.StatusCode = 400;
                }
                else
                {
                    newObject.data = tmpContent;
                }
            }


            if (isDebug)
            {

            }
            else
            {
                string sign_server = GetMd5Hash(string.Join("|", token, version, localTime, newObject.data, sToken));
                if (sign_server != sign)
                {
                    context.Response.StatusCode = 400;
                    return;
                }
            }


            newObject.KASProductName = newObject.KASProductName.ToLower();


            content = JsonConvert.SerializeObject(newObject);
            context.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));


            if (newObject.isNeedEncrypt)
            {
                Stream responseBody = context.Response.Body;
                using (var newResponseBody = new MemoryStream())
                {
                    context.Response.Body = newResponseBody;
                    await next(context);
                    context.Response.Body = new MemoryStream();
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    context.Response.Body = responseBody;
                    string html = new StreamReader(newResponseBody).ReadToEnd();
                    html = Encrypt_Compress(html, xToken);
                    await context.Response.WriteAsync(html);
                }
            }
            else
            {
                await next(context);
            }


        }
        string GetMd5Hash(string input)
        {
            byte[] buffer = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                string str = ((byte)buffer[i]).ToString("X").ToLower();
                builder.Append(((str.Length == 1) ? "0" : "") + str);
            }
            return builder.ToString();
        }

        string Encrypt_Compress(string data, byte XorKey)
        {

            string rs = "";
            try
            {
                var bufferIn = CompressData(data);

                for (int i = 0; i < bufferIn.Length; i++)
                {
                    bufferIn[i] ^= XorKey;
                }


                rs = Convert.ToBase64String(bufferIn);
            }
            catch { }
            return rs;

        }
        string Decrypt_Decompress(string data, byte XorKey)
        {
            string rs = "";
            try
            {
                var bufferIn = Convert.FromBase64String(data);
                for (int i = 0; i < bufferIn.Length; i++)
                {
                    bufferIn[i] ^= XorKey;
                }
                rs = DecompressData(bufferIn);
            }
            catch { }

            return rs;

        }
        byte[] CompressData(string data)
        {
            try
            {


                byte[] buffer = Encoding.UTF8.GetBytes(data);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    MemoryStream outFile = new MemoryStream();

                    using (GZipStream Compress =
                        new GZipStream(outFile,
                        CompressionMode.Compress))
                    {
                        // Copy the source file into 
                        // the compression stream.
                        ms.CopyTo(Compress);

                    }
                    return outFile.ToArray();


                }
            }
            catch { }
            return null;
        }
        public static string DecompressData(byte[] bufferIn)
        {

            string json = string.Empty;
            try
            {


                using (MemoryStream msIn = new MemoryStream(bufferIn))
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        GZipStream Decompress = new GZipStream(msIn,
                                CompressionMode.Decompress);

                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(ms);
                        json = ASCIIEncoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch { }
            return json;
        }
    }
}