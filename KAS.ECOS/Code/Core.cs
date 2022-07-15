using Newtonsoft.Json;
using System.IO.Compression;
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
            /// <param name="str"></param>
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
            public static string TaoMatKhau(string username, string password)
            {
                return StringMD5(string.Format("{0}{1}{2}", password, username, "mip"));
            }
            public static string TaoCardId(string username)
            {
                return (username + "mip").GetHashCode().ToString("X").ToLower();
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
        }

     
    }
}
