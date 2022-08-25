using KAS.API.MIDDEWARE.Entity;
using KAS.ECOS.API.Entity;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace KAS.ECOS.API.Code
{
    public class Core
    {
        public class CoreString
        {
            public static string StringIdentity(string str)
            {

                System.Text.RegularExpressions.MatchCollection matchCollection = System.Text.RegularExpressions.Regex.Matches(str, "[0-9]{1,}");
                try
                {
                    System.Text.RegularExpressions.Match match = matchCollection[matchCollection.Count - 1];
                    string beginStr = str.Substring(0, match.Index);

                    uint number = Convert.ToUInt32(str.Substring(match.Index, match.Length)) + 1;
                    return string.Format("{0}{1}", beginStr, number.ToString().PadLeft(match.Length, '0'));
                }
                catch
                {
                    return string.Format("{0}{1}", str, 1);
                }
            }

            /// <summary>
            /// Trả về số nguyên cuối cùng nằm trong chuỗi
            /// </summary>
            /// <param name="str">Chuỗi ký tự</param>
            /// <returns></returns>
            public static uint StringInterger(string str)
            {

                System.Text.RegularExpressions.MatchCollection matchCollection = System.Text.RegularExpressions.Regex.Matches(str, "[0-9]{1,}");
                try
                {
                    System.Text.RegularExpressions.Match match = matchCollection[matchCollection.Count - 1];
                    string beginStr = str.Substring(0, match.Index);

                    return Convert.ToUInt32(str.Substring(match.Index, match.Length));
                }
                catch
                {
                    return 0;
                }
            }
            /// <summary>
            /// Mã hóa chuỗi ký tự thành chuỗi MD5
            /// </summary>
            /// <param name="str">Chuỗi ký tự</param>
            /// <returns></returns>
            public static string StringMD5(string str)
            {

                try
                {
                    byte[] myDate = System.Text.UnicodeEncoding.ASCII.GetBytes(str);
                    System.Security.Cryptography.MD5CryptoServiceProvider m = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] r = m.ComputeHash(myDate);
                    return BitConverter.ToString(r).Replace("-", "").ToLower();
                }
                catch (Exception ee)
                {
                    Log4Net.WriteLogError("StringMD5", ee);
                    return null;
                }
            }
            /// <summary>
            /// Tạo mật khẩu
            /// </summary>
            /// <param name="username">tên đăng nhập</param>
            /// <param name="password">mật khẩu người dùng nhập</param>
            /// <returns></returns>
            public static string TaoMatKhau(string username, string password)
            {
                return StringMD5(string.Format("{0}{1}{2}", password, username, "mip"));
            }
            public static string TaoCardId(string username)
            {
                return (username + "mip").GetHashCode().ToString("X").ToLower();
            }
            /// <summary>
            /// Tạo chuỗi số random
            /// </summary>
            /// <param name="length">Số ký tự cần random</param>
            /// <returns></returns>
            public static string RandomNumber(int length)
            {
                string chars = "0123456789";
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(10));
                var random = new Random();
                string result = "";
                for (int i = 0; i < length; i++)
                {
                    result += chars[random.Next(chars.Length)];
                }
                return result;
            }
        }
        public class Encryptor
        {
            // Fields

            // Methods
            public static byte[] Decrypt(byte[] encryptedData, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
            {
                return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            }

            public static string DecryptString(string secretKey, string encryptedText)
            {
                try
                {
                    byte[] encryptedData = Convert.FromBase64String(encryptedText.Replace(" ", "+").Replace("-", "+").Replace('_', '/'));
                    return ASCIIEncoding.UTF8.GetString(Decrypt(encryptedData, GetRijndaelManaged(secretKey)));
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public static byte[] Encrypt(byte[] plainBytes, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
            {
                return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }

            public static string EncryptString(string secretKey, string plainText)
            {
                return Convert.ToBase64String(Encrypt(ASCIIEncoding.UTF8.GetBytes(plainText), GetRijndaelManaged(secretKey)));
            }

            public static string GetMd5Hash(string input)
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

            public static System.Security.Cryptography.RijndaelManaged GetRijndaelManaged(string secretKey)
            {
                byte[] buffer = new byte[0x10];
                byte[] bytes = ASCIIEncoding.UTF8.GetBytes(secretKey);
                Array.Copy(bytes, buffer, Math.Min((int)buffer.Length, (int)bytes.Length));
                System.Security.Cryptography.RijndaelManaged managed1 = new System.Security.Cryptography.RijndaelManaged();
                managed1.Mode = System.Security.Cryptography.CipherMode.CBC;
                managed1.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                managed1.KeySize = 0x80;
                managed1.BlockSize = 0x80;
                managed1.Key = buffer;
                managed1.IV = buffer;
                return managed1;
            }


        }
        public class MaHoa
        {
            #region Mã hóa
            public static string strRC2Encryption(string strInput)
            {
                return Core.MaHoa.strRC2Encryption(strInput, "!123mip_iCafe", "~123mip_iCafe");
            }

            public static string strRC2Decryption(string strInput)
            {
                return Core.MaHoa.strRC2Decryption(strInput, "!123mip_iCafe", "~123mip_iCafe");
            }
            public static string strRC2Encryption(string strInput, string strKey, string strIV)
            {
                string strReturn = string.Empty;
                try
                {

                    byte[] byteInput = Encoding.UTF8.GetBytes(strInput);
                    byte[] byteKey = Encoding.ASCII.GetBytes(strKey);
                    byte[] byteIV = Encoding.ASCII.GetBytes(strIV);
                    System.IO.MemoryStream MS = new System.IO.MemoryStream();
                    System.Security.Cryptography.RC2CryptoServiceProvider CryptoMethod = new System.Security.Cryptography.RC2CryptoServiceProvider();
                    System.Security.Cryptography.CryptoStream CS = new System.Security.Cryptography.CryptoStream(MS, CryptoMethod.CreateEncryptor(byteKey, byteIV), System.Security.Cryptography.CryptoStreamMode.Write);
                    CS.Write(byteInput, 0, byteInput.Length);
                    CS.FlushFinalBlock();
                    CS.Close();
                    CryptoMethod.Clear();
                    strReturn = Convert.ToBase64String(MS.ToArray());

                }
                catch (Exception ee)
                {
                    Log4Net.WriteLogError("strRC2Encryption", ee);
                    strReturn = string.Empty;
                }
                return strReturn;
            }
            public static byte[] strRC2Encryption(byte[] byteInput, string strKey, string strIV)
            {
                try
                {
                    byte[] byteKey = Encoding.ASCII.GetBytes(strKey);
                    byte[] byteIV = Encoding.ASCII.GetBytes(strIV);
                    System.IO.MemoryStream MS = new System.IO.MemoryStream();

                    System.Security.Cryptography.RC2CryptoServiceProvider CryptoMethod = new System.Security.Cryptography.RC2CryptoServiceProvider();
                    System.Security.Cryptography.CryptoStream CS = new System.Security.Cryptography.CryptoStream(MS, CryptoMethod.CreateEncryptor(byteKey, byteIV), System.Security.Cryptography.CryptoStreamMode.Write);
                    CS.Write(byteInput, 0, byteInput.Length);
                    CS.FlushFinalBlock();
                    CS.Close();
                    CryptoMethod.Clear();
                    return MS.ToArray();

                }
                catch (Exception ee)
                {
                    Log4Net.WriteLogError("strRC2Encryption", ee);
                    return null;
                }
            }
            public static string strRC2Decryption(string strInput, string strKey, string strIV)
            {
                try
                {
                    byte[] byteInput = Convert.FromBase64String(strInput);
                    byte[] byteKey = Encoding.ASCII.GetBytes(strKey);
                    byte[] byteIV = Encoding.ASCII.GetBytes(strIV);
                    System.IO.MemoryStream MS = new System.IO.MemoryStream();

                    System.Security.Cryptography.RC2CryptoServiceProvider RC2 = new System.Security.Cryptography.RC2CryptoServiceProvider();
                    System.Security.Cryptography.CryptoStream CS = new System.Security.Cryptography.CryptoStream(MS, RC2.CreateDecryptor(byteKey, byteIV), System.Security.Cryptography.CryptoStreamMode.Write);
                    CS.Write(byteInput, 0, byteInput.Length);
                    CS.FlushFinalBlock();
                    CS.Close();
                    RC2.Clear();
                    return Encoding.UTF8.GetString(MS.ToArray());

                }
                catch (Exception ee)
                {
                    Log4Net.WriteLogError("strRC2Decryption", ee);
                    return string.Empty;
                }
            }
            public static byte[] strRC2Decryption(byte[] byteInput, string strKey, string strIV)
            {
                try
                {
                    byte[] byteKey = Encoding.ASCII.GetBytes(strKey);
                    byte[] byteIV = Encoding.ASCII.GetBytes(strIV);
                    System.IO.MemoryStream MS = new System.IO.MemoryStream();

                    System.Security.Cryptography.RC2CryptoServiceProvider RC2 = new System.Security.Cryptography.RC2CryptoServiceProvider();
                    System.Security.Cryptography.CryptoStream CS = new System.Security.Cryptography.CryptoStream(MS, RC2.CreateDecryptor(byteKey, byteIV), System.Security.Cryptography.CryptoStreamMode.Write);
                    CS.Write(byteInput, 0, byteInput.Length);
                    CS.FlushFinalBlock();
                    CS.Close();
                    RC2.Clear();
                    return MS.ToArray();

                }
                catch (Exception ee)
                {
                    Log4Net.WriteLogError("strRC2DecryptionbyteInput", ee);
                    return null;
                }
            }

            #endregion
        }
        public class Log4Net
        {
            #region Log4Net
            public static void WriteLogDebug(string fullname, string msg)
            {
                try
                {
                    
                }
                catch { }
            }
            public static void WriteLogError(string fullname, object error)
            {
                try
                {
                   
                }
                catch { }
            }
            #endregion
        }
        public class DataHandle
        {
            public static class XmlHandle
            {
                public static object Load(string FileName, Type type)
                {
                    try
                    {

                        System.Xml.XmlTextReader xml = new System.Xml.XmlTextReader(FileName);
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                        object obj = serializer.Deserialize(xml);
                        xml.Close();
                        return obj;
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("XmlHandle", ee);
                        return null;
                    }
                }
                public static object LoadExt(string text, Type type)
                {
                    try
                    {
                        byte[] array = ASCIIEncoding.Unicode.GetBytes(text);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(array);
                        System.Xml.XmlTextReader xml = new System.Xml.XmlTextReader(ms);


                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                        object obj = serializer.Deserialize(xml);
                        xml.Close();

                        return obj;
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("LoadExt", ee);
                        return null;
                    }
                }
                public static object Save(string FileName, object m)
                {
                    try
                    {
                        System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(FileName, ASCIIEncoding.Unicode);
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(m.GetType());
                        serializer.Serialize(xml, m);
                        xml.Close();
                        return null;

                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("Save", ee);
                        return null;
                    }
                }
                /// <summary>
                /// Trả về một chuỗi sau khi serializer
                /// </summary>
                /// <param name="FileName"></param>
                /// <param name="m"></param>
                /// <returns></returns>
                public static string Save(object m)
                {
                    try
                    {
                        StringWriter stringWriter = new StringWriter();

                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(m.GetType());
                        serializer.Serialize(stringWriter, m);

                        return stringWriter.ToString();
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("Save", ee);
                        return null;
                    }
                }
            }
            public class GZip
            {
                public static void Compress(string filemane, string outputName)
                {
                    try
                    {
                        // Get the stream of the source file.
                        System.IO.FileInfo fi = new FileInfo(filemane);
                        using (FileStream inFile = fi.OpenRead())
                        {
                            // Prevent compressing hidden and 
                            // already compressed files.
                            if ((File.GetAttributes(fi.FullName)
                                & FileAttributes.Hidden)
                                != FileAttributes.Hidden & fi.Extension != ".Dat")
                            {
                                // Create the compressed file.
                                FileStream outFile =
                                            File.Create(outputName);

                                using (GZipStream Compress =
                                    new GZipStream(outFile,
                                    CompressionMode.Compress))
                                {
                                    // Copy the source file into 
                                    // the compression stream.
                                    inFile.CopyTo(Compress);
                                }

                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("Compress", ee);
                    }
                }
                public static string Json1(object data)
                {
                    try
                    {
                        string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                        return json;
                    }
                    catch (Exception ee)
                    {

                        Log4Net.WriteLogError("Json1", ee);
                        return null;
                    }
                }
                public static T Json2<T>(string data)
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }

                public static void CompressData(object data, string outputName)
                {
                    try
                    {
                        using (FileStream fs = File.Open(outputName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                        {
                            byte[] bytes = CompressData(data);
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                        }
                        //   System.IO.File.WriteAllBytes(outputName, CompressData(data));


                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("CompressData", ee);
                    }
                }
                public static byte[] CompressData(object data)
                {
                    try
                    {
                        // Get the stream of the source file.
                        string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });

                        byte[] buffer = ASCIIEncoding.UTF8.GetBytes(json);

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
                            return Core.MaHoa.strRC2Encryption(outFile.GetBuffer(), "!123mip_iCafe", "~123mip_iCafe");


                        }
                    }
                    catch (Exception ee)
                    {

                        Log4Net.WriteLogError("CompressData", ee);
                        return null;
                    }
                }

                public static T DecompressData<T>(string filename)
                {
                    System.IO.FileInfo fi = new FileInfo(filename);
                    // Get the stream of the source file.
                    string json = string.Empty;
                    try
                    {
                        byte[] bufferIn = null;
                        using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            bufferIn = new byte[fs.Length];
                            fs.Read(bufferIn, 0, (int)fs.Length);
                            // StreamReader sr = new StreamReader(fs);
                            // FileInfo fi = new FileInfo(filename);
                            //BinaryReader br = new BinaryReader(fs);
                            //bufferIn = br.ReadBytes((int)fs.Length);

                            ////  sr.Close();
                            //br.Close();
                            fs.Close();

                        }
                        //                        byte[] bufferIn = System.IO.File.ReadAllBytes(filename);
                        bufferIn = Core.MaHoa.strRC2Decryption(bufferIn, "!123mip_iCafe", "~123mip_iCafe");

                        using (MemoryStream msIn = new MemoryStream(bufferIn))
                        {

                            // Get original file extension, for example
                            // "doc" from report.doc.gz.
                            // string curFile = fi.FullName;
                            //string origName = curFile.Remove(curFile.Length -
                            //        fi.Extension.Length);

                            //Create the decompressed file.
                            using (MemoryStream ms = new MemoryStream())
                            {
                                GZipStream Decompress = new GZipStream(msIn,
                                        CompressionMode.Decompress);

                                // Copy the decompression stream 
                                // into the output file.
                                Decompress.CopyTo(ms);


                                json = ASCIIEncoding.UTF8.GetString(ms.GetBuffer());
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("DecompressData", ee);
                    }
                    return JsonConvert.DeserializeObject<T>(json);
                }
                public static T DecompressData<T>(byte[] bufferIn)
                {

                    string json = string.Empty;
                    try
                    {

                        bufferIn = Core.MaHoa.strRC2Decryption(bufferIn, "!123mip_iCafe", "~123mip_iCafe");

                        using (MemoryStream msIn = new MemoryStream(bufferIn))
                        {

                            // Get original file extension, for example
                            // "doc" from report.doc.gz.
                            // string curFile = fi.FullName;
                            //string origName = curFile.Remove(curFile.Length -
                            //        fi.Extension.Length);

                            //Create the decompressed file.
                            using (MemoryStream ms = new MemoryStream())
                            {
                                GZipStream Decompress = new GZipStream(msIn,
                                        CompressionMode.Decompress);

                                // Copy the decompression stream 
                                // into the output file.
                                Decompress.CopyTo(ms);


                                json = ASCIIEncoding.UTF8.GetString(ms.GetBuffer());
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        Log4Net.WriteLogError("DecompressData", ee);
                    }
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }

            public class Json
            {
                public static string Json_SerializeObject(object data)
                {
                    try
                    {
                        string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto,

                        });
                        return json;
                    }
                    catch
                    {

                        return null;
                    }
                }
                public static T Json_DeserializeObject<T>(string data)
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                public static void SaveJson(object data, string filename)
                {
                    try
                    {
                        using (System.IO.StreamWriter sw = new StreamWriter(filename))
                        {
                            //   string s111 = Json_SerializeObject(data);
                            sw.Write(Json_SerializeObject(data));
                            sw.Close();
                        }
                    }
                    catch
                    { }
                }
                public static T LoadJson<T>(string filename)
                {
                    string data = string.Empty;
                    try
                    {
                        using (System.IO.StreamReader sw = new StreamReader(filename))
                        {
                            data = sw.ReadToEnd();
                            sw.Close();
                        }

                    }
                    catch { }
                    return Json_DeserializeObject<T>(data);
                }

            }

            public class CallAPI
            {
                /// <summary>
                /// Lấy thông tin HTTP request
                /// </summary>
                /// <param name="authen">token trong header</param>
                /// <returns></returns>
                private static HttpClient GetClient(string authen = "")
                {
                    HttpClientHandler hanldeMain = new HttpClientHandler();
                    hanldeMain.AllowAutoRedirect = false;
                    hanldeMain.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    hanldeMain.UseProxy = false;
                    hanldeMain.CookieContainer = new CookieContainer();
                    hanldeMain.Credentials = CredentialCache.DefaultCredentials;
                    HttpClient client = new HttpClient(hanldeMain);
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

                    if (!string.IsNullOrEmpty(authen))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", authen);
                    }

                    return client;
                }
                /// <summary>
                /// Gọi API băng phương thức GET
                /// </summary>
                /// <param name="url">link api</param>
                /// <param name="authen">token trong header</param>
                /// <returns></returns>
                public static async Task<(HttpStatusCode, string)> GetAsync(string url, string authen)
                {
                    try
                    {
                        var client = GetClient(authen);
                        var responseResult = await client.GetAsync(url);

                        return (responseResult.StatusCode, await responseResult.Content.ReadAsStringAsync());
                    }
                    catch (Exception ee)
                    {
                        return (HttpStatusCode.BadGateway, ee.Message);
                    }
                }
                /// <summary>
                /// Gọi API băng phương thức POS
                /// </summary>
                /// <param name="url">link api</param>
                /// <param name="json">data payload</param>
                /// <param name="authen">token trong header</param>
                /// <returns></returns>
                public static async Task<(HttpStatusCode, string)> PostAsync(string url, string json, string authen)
                {
                    try
                    {
                        var client = GetClient(authen);
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var responseResult = await client.PostAsync(url, content);

                        return (responseResult.StatusCode, await responseResult.Content.ReadAsStringAsync());
                    }
                    catch (Exception ee)
                    {
                        return (HttpStatusCode.BadGateway, ee.Message);
                    }
                }
                /// <summary>
                /// Gọi API băng phương thức PUT
                /// </summary>
                /// <param name="url">link api</param>
                /// <param name="json">data payload</param>
                /// <param name="authen">token trong header</param>
                /// <returns></returns>
                public static async Task<(HttpStatusCode, string)> PutAsync(string url, string json, string authen)
                {
                    try
                    {
                        var client = GetClient(authen);
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        var responseResult = await client.PutAsync(url, content);

                        return (responseResult.StatusCode, await responseResult.Content.ReadAsStringAsync());
                    }
                    catch (Exception ee)
                    {
                        return (HttpStatusCode.BadGateway, ee.Message);
                    }
                }
                /// <summary>
                /// Gọi API băng phương thức DELETE
                /// </summary>
                /// <param name="url">link api</param>
                /// <param name="json">data payload</param>
                /// <param name="authen">token trong header</param>
                /// <returns></returns>
                public static async Task<(HttpStatusCode, string)> DeleteAsync(string url, string json, string authen)
                {
                    try
                    {
                        var client = GetClient(authen);

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri(url),
                            Content = new StringContent(json, Encoding.UTF8, "application/json")
                        };
                        var responseResult = await client.SendAsync(request);
                        return (responseResult.StatusCode, await responseResult.Content.ReadAsStringAsync());
                    }
                    catch (Exception ee)
                    {
                        return (HttpStatusCode.BadGateway, ee.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Gửi SMS, OTP cho các đơn vị cung cấp dịch vụ SMS
        /// Chưa xử lý cấu hình lấy từ DB
        /// CMC, ESMS chỉ mới xử lý gửi OTP
        /// </summary>
        public class SMS
        {
            private const string SendFalse = "Gửi OTP không thành công!";
            private const string ConfirmFalse = "OTP không hợp lệ!";
            private const string PhoneInvalid = "Không có số điện thoại!";
            private const string RecapcharInvalid = "Không có mã recapchar!";
            private const string OTPInvalid = "Không có mã OTP!";
            private const string SessionInvalid = "Không có mã sesion!";

            /// <summary>
            /// Chuyển đổi số điện thoại và mã quốc gia về đúng định dạng
            /// </summary>
            /// <param name="phone">Số điện thoại</param>
            /// <param name="prefix">Mã số quốc gia</param>
            /// <returns></returns>
            public static string GetPhone(string phone, string prefix)
            {
                if (string.IsNullOrEmpty(phone))
                    phone = "";
                if (string.IsNullOrEmpty(prefix))
                    prefix = "";
                
                prefix = prefix.Replace("+", "").TrimStart('0');
                if(phone.IndexOf(prefix) == 0 || phone.IndexOf($"+{prefix}") == 0)
                {
                    int position = phone.IndexOf(prefix) == 0 ? prefix.Length : prefix.Length + 1;
                    phone = phone.Substring(position, phone.Length - position);
                }
                phone = $"+{prefix}{phone.TrimStart('0').Replace(" ", "")}";

                if (phone.Length < 11)
                    phone = "";

                return phone;
            }
            /// <summary>
            /// loại bỏ mã số vùng 
            /// </summary>
            /// <param name="phone"></param>
            /// <returns></returns>
            public static string GetPhoneNotRegions(string phone, string prefix)
            {
                if (!string.IsNullOrEmpty(phone))
                {
                    if (string.IsNullOrEmpty(prefix))
                        prefix = "";

                    phone = GetPhone(phone, prefix);
                    int length = prefix.Length + 1;
                    if (!string.IsNullOrEmpty(phone))
                        phone = $"0{phone.Substring(length, phone.Length - length)}";
                }
                else
                    phone = "";

                return phone;
            }

            /// <summary>
            /// Gửi SMS cho Firebase
            /// </summary>
            public class Firebase
            {
                /// <summary>
                /// Lấy cấu hình gửi SMS
                /// </summary>
                /// <returns></returns>
                public static SMSConfigFirebase GetConfig()
                {
                    return new SMSConfigFirebase()
                    {
                        SiteId = "CENTER",
                        Token = "AIzaSyDtJezxUkbYK85nTrYJB5nP8je4wqwabME",
                        AuthDomain = "notifycation-ffa82.firebaseapp.com",
                        ProjectId = "notifycation-ffa82",
                        StorageBucket = "notifycation-ffa82.appspot.com",
                        MessagingSenderId = "69106568612",
                        AppId = "1:69106568612:web:27d0e3e3fc340188ee4531",
                        MeasurementId = "G-7VT9SV08DZ",
                        UrlAuth = "https://sales.kas.asia",
                        Content = "",
                    };
                }
                /// <summary>
                /// Kiểm tra và trả về string json responses
                /// </summary>
                /// <param name="statusCode"></param>
                /// <param name="json"></param>
                /// <returns></returns>
                private static string GetJSON(System.Net.HttpStatusCode statusCode, string json)
                {
                    if (statusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(json))
                        return json;
                    return "";
                }
                /// <summary>
                /// Gửi SMS
                /// </summary>
                /// <param name="phone">Số điện thoại</param>
                /// <param name="recapcharToke">Recapchar của client</param>
                /// <returns></returns>
                public static async Task<OutEntity> Send(string phone, string recapcharToke)
                {
                    OutEntity res = new OutEntity("", SendFalse);
                    try
                    {
                        if(string.IsNullOrEmpty(phone))
                            return new OutEntity("", PhoneInvalid);
                        if(string.IsNullOrEmpty(recapcharToke))
                            return new OutEntity("", RecapcharInvalid);

                        //lấy cấu hình firebase
                        SMSConfigFirebase config = Firebase.GetConfig();
                        phone = GetPhone(phone, "+84");
                        string linkAPI = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/sendVerificationCode?key={config.Token}";

                        //Gửi data qua firebase
                        var resAPI = await DataHandle.CallAPI.PostAsync(
                            linkAPI,
                            DataHandle.Json.Json_SerializeObject(new
                            {
                                phoneNumber = phone,
                                recaptchaToken = recapcharToke
                            }),
                            ""
                        );

                        //xử lý dữ liệu trả về
                        var dataRes = JsonConvert.DeserializeAnonymousType(
                            GetJSON(resAPI.Item1, resAPI.Item2),
                            new
                            {
                                sessionInfo = ""
                            }
                        );
                        if (!string.IsNullOrEmpty(dataRes?.sessionInfo))
                            res = new OutEntity(dataRes.sessionInfo, "");
                    }
                    catch(Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
                /// <summary>
                /// Xác nhận OTP SMS
                /// </summary>
                /// <param name="otp">Mã OTP</param>
                /// <param name="sessionInfo">Session được trả về từ api Send</param>
                /// <returns></returns>
                public static async Task<OutEntity> Confirm(string otp, string sessionInfo)
                {
                    OutEntity res = new OutEntity("", ConfirmFalse);
                    try
                    {
                        if (string.IsNullOrEmpty(otp))
                            return new OutEntity("", OTPInvalid);
                        if (string.IsNullOrEmpty(sessionInfo))
                            return new OutEntity("", SessionInvalid);

                        //lấy cấu hình firebase
                        SMSConfigFirebase config = Firebase.GetConfig();
                        string linkAPI = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPhoneNumber?key={config.Token}";

                        //Gửi data qua firebase
                        var resAPI = await DataHandle.CallAPI.PostAsync(
                            linkAPI,
                            DataHandle.Json.Json_SerializeObject(new
                            {
                                sessionInfo = sessionInfo,
                                code = otp
                            }),
                            ""
                        );

                        //xử lý dữ liệu trả về
                        var dataRes = JsonConvert.DeserializeAnonymousType(
                            GetJSON(resAPI.Item1, resAPI.Item2),
                            new
                            {
                                idToken = "",
                                refreshToken = "",
                                expiresIn = "",
                                localId = "",
                                isNewUser = "",
                                phoneNumber = "",
                            }
                        );
                        if (!string.IsNullOrEmpty(dataRes?.idToken))
                            res = new OutEntity(dataRes.idToken, "");
                    }
                    catch (Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
            }

            /// <summary>
            /// Gửi SMS cho ESMS
            /// </summary>
            public class ESMS
            {
                /// <summary>
                /// Lấy cấu hình ESMS
                /// </summary>
                /// <returns></returns>
                public static SMSConfigESMS GetConfig()
                {
                    return new SMSConfigESMS()
                    {
                        SiteId = "CENTER",
                        Content = "ChickenPlus thong bao ma xac thuc cho tai khoan cua ban la $OTP$. Luu y: KHONG chia se ma nay voi nguoi khac.",
                        BrandName = "ChickenPlus",
                        ApiKey = "895ED159983BD48978E0E30CA58D07",
                        SecretKey = "49FBD948B41C1B27A3A6CA4FE90CFC",
                        SmsType = 2,
                        IsUnicode = 0
                    };
                }
                /// <summary>
                /// Kiểm tra và trả về string json responses
                /// </summary>
                /// <param name="statusCode"></param>
                /// <param name="json"></param>
                /// <returns></returns>
                private static string GetJSON(System.Net.HttpStatusCode statusCode, string json)
                {
                    if (statusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(json))
                        return json;
                    return "";
                }
                /// <summary>
                /// Gửi SMS
                /// </summary>
                /// <param name="phone">Số điện thoại</param>
                /// <returns></returns>
                public static async Task<OutEntity> Send(string phone)
                {
                    OutEntity res = new OutEntity("", SendFalse);
                    try
                    {
                        if (string.IsNullOrEmpty(phone))
                            return new OutEntity("", PhoneInvalid);

                        //lấy cấu hình
                        SMSConfigESMS config = GetConfig();
                        string OTP = CoreString.RandomNumber(6);
                        string content = config.Content.Replace("$OTP$", OTP);
                        string linkAPI = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_post_json";

                        //Gửi data qua ESMS
                        var resAPI = await DataHandle.CallAPI.PostAsync(
                            linkAPI,
                            DataHandle.Json.Json_SerializeObject(new
                            {
                                ApiKey = config.ApiKey,
                                SecretKey = config.SecretKey,
                                Brandname = config.BrandName,
                                SmsType = config.SmsType,
                                IsUnicode = config.IsUnicode,
                                Phone = phone,
                                Content = content
                            }),
                            ""
                        );

                        //xử lý dữ liệu trả về
                        var dataRes = JsonConvert.DeserializeAnonymousType(
                            GetJSON(resAPI.Item1, resAPI.Item2),
                            new
                            {
                                CodeResult = "",
                                CountRegenerate = 0,
                                SMSID = ""
                            }
                        );
                        if (!string.IsNullOrEmpty(dataRes?.CodeResult) && dataRes.CodeResult == "100")
                            res = new OutEntity(OTP, "");
                    }
                    catch (Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
                /// <summary>
                /// Xác nhận OTP SMS
                /// </summary>
                /// <param name="phone">Số điện thoại</param>
                /// <param name="otp">Mã OTP</param>
                /// <returns></returns>
                public static async Task<OutEntity> Confirm(string phone, string otp)
                {
                    OutEntity res = new OutEntity("", ConfirmFalse);
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
            }

            /// <summary>
            /// Gửi SMS cho CMC
            /// </summary>
            public class CMC
            {
                /// <summary>
                /// Lấy cấu hình CMC
                /// </summary>
                /// <returns></returns>
                public static SMSConfigCMC GetConfig()
                {
                    return new SMSConfigCMC()
                    {
                        SiteId = "CENTER",
                        Content = "$OTP$ la ma OTP xac nhan dang ky tai khoan cua ban. Cam on da luon dong hanh cung Hot&Cold, Hotline 19002230 .",
                        BrandName = "Hot cold",
                        Username = "hotcold",
                        Password = "UCTwgA8u",
                        IsUnicode = 0
                    };
                }
                /// <summary>
                /// Kiểm tra và trả về string json responses
                /// </summary>
                /// <param name="statusCode"></param>
                /// <param name="json"></param>
                /// <returns></returns>
                private static string GetJSON(System.Net.HttpStatusCode statusCode, string json)
                {
                    if (statusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(json))
                        return json;
                    return "";
                }
                /// <summary>
                /// Gửi SMS
                /// </summary>
                /// <param name="phone">Số điện thoại</param>
                /// <returns></returns>
                public static async Task<OutEntity> Send(string phone)
                {
                    OutEntity res = new OutEntity("", SendFalse);
                    try
                    {
                        if (string.IsNullOrEmpty(phone))
                            return new OutEntity("", PhoneInvalid);

                        //lấy cấu hình
                        SMSConfigCMC config = GetConfig();
                        string OTP = CoreString.RandomNumber(6);
                        string content = config.Content.Replace("$OTP$", OTP);
                        string linkAPI = "http://124.158.14.49/CMC_RF/api/sms/";

                        (HttpStatusCode, string) resAPI;

                        if (config.IsUnicode == 0)
                        {
                            linkAPI += "Send";
                            resAPI = await DataHandle.CallAPI.PostAsync(
                            linkAPI,
                            DataHandle.Json.Json_SerializeObject(new
                            {
                                Brandname = config.BrandName,
                                Phonenumber = phone,
                                Message = content,
                                user = config.Username,
                                pass = config.Password
                            }),
                            ""
                        );
                        }
                        else
                        {
                            linkAPI += "sendUTF";
                            resAPI = await DataHandle.CallAPI.PostAsync(
                              linkAPI,
                              DataHandle.Json.Json_SerializeObject(new
                              {
                                  Brandname = config.BrandName,
                                  Phonenumber = phone,
                                  Message = content,
                                  user = config.Username,
                                  pass = config.Password,
                                  SendTime = (new DateTime()).ToString("yyyy-MM-dd HH:mm:ss")
                              }),
                              ""
                            );
                        }

                        //xử lý dữ liệu trả về
                        var dataRes = JsonConvert.DeserializeAnonymousType(
                            GetJSON(resAPI.Item1, resAPI.Item2),
                            new
                            {
                                Code = "",
                                Description = "",
                                Data = new
                                {
                                    Brandname = "",
                                    Phonenumber = "",
                                    Message = "",
                                    Status = "",
                                    StatusDescription = ""
                                }
                            }
                        );
                        if (!string.IsNullOrEmpty(dataRes?.Code) && dataRes.Code == "1")
                            res = new OutEntity(OTP, "");
                    }
                    catch (Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
                /// <summary>
                /// Xác nhận OTP SMS
                /// </summary>
                /// <param name="phone">Số điện thoại</param>
                /// <param name="otp">Mã OTP</param>
                /// <returns></returns>
                public static async Task<OutEntity> Confirm(string phone, string otp)
                {
                    OutEntity res = new OutEntity("", ConfirmFalse);
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        res = new OutEntity("", ex.Message);
                    }

                    return res;
                }
            }
        }
    }
}
