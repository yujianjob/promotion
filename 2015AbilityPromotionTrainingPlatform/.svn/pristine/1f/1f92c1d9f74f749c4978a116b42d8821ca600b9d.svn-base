using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Dianda.Common.StringSecurity
{


    /// <summary> 
    /// 加密
    /// </summary> 
    public class AES
    {
        //默认密钥向量
        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
        private static Encoding encode = Encoding.UTF8;
        private static string defaultKey = "AFEIAFEIEHOEWQAJFEIFEIYZXVEFEWRE";
        public static string Encrypt(string encryptString)
        {
            return Encrypt(encryptString, defaultKey);
        }
        public static string Decrypt(string decryptString)
        {
            return Decrypt(decryptString, defaultKey);
        }
        public static string Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = Utils.GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = encode.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = encode.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string decryptString, string decryptKey)
        {
            if (decryptString == string.Empty)
            {
                return string.Empty;
            }
            try
            {
                decryptKey = Utils.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = encode.GetBytes(decryptKey);
                rijndaelProvider.IV = Keys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return encode.GetString(decryptedData);
            }
            catch
            {
                return "";
            }

        }

    }

    /// <summary> 
    /// 加密
    /// </summary> 
    public class DES
    {
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static Encoding encode = Encoding.UTF8;
        private static string defaultKey = "FEQRETYU";
        public static string Encrypt(string encryptString)
        {
            return Encrypt(encryptString, defaultKey);
        }
        public static string Decrypt(string decryptString)
        {
            return Decrypt(decryptString, defaultKey);
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = Utils.GetSubString(encryptKey, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = encode.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = encode.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            /*
            string a = Convert.ToBase64String(mStream.ToArray());
            a = a.Replace("/", "_");

            return a;
             * */

            StringBuilder ret = new StringBuilder();
            foreach (byte b in mStream.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = Utils.GetSubString(decryptKey, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = encode.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                /*
                string a = decryptString;
                a = a.Replace("_", "/");
                 * */

                byte[] inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                //byte[] inputByteArray = Convert.FromBase64String(a);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return encode.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }



    }
    public class MD5Lib
    {
        private static Encoding encode = Encoding.UTF8;
        private static MD5 md5 = new MD5CryptoServiceProvider();

        public MD5Lib()
        {
        }

        private static string MD5ByteToStr(byte[] b)
        {
            string result = "";
            for (int i = 0; i < b.Length; i++)
            {
                result += b[i].ToString("X2");
            }
            return result;
        }

        //public static string CalcFileMD5(string fileName)
        //{
        //    Stream stream = File.OpenRead(fileName);
        //    return CalcStreamMD5(stream);
        //}

        //public static string CalcStreamMD5(Stream stream)
        //{
        //    byte[] md5Hash = md5.ComputeHash(stream);
        //    return MD5ByteToStr(md5Hash);
        //}

        //调用该方法即可。
        public static string Encrypt(string str)
        {
            return Encrypt(str, encode);
        }

        public static string Encrypt(string str, Encoding encoding)
        {
            byte[] source = encoding.GetBytes(str);
            byte[] md5Hash = md5.ComputeHash(source);
            return MD5ByteToStr(md5Hash);
        }


    }

    public class Base64
    {
        public static string Encode(string str)
        {
            return Encode(str, Encoding.UTF8);
        }


        public static string Encode(string str, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(str);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }

        }

        public static string Decode(string str, Encoding encoding)
        {
            byte[] outputb = Convert.FromBase64String(str);
            try
            {
                return encoding.GetString(outputb);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Decode(string str)
        {
            return Decode(str, Encoding.UTF8);
        }

    }

    public class AES128
    {

        /// <summary>
        /// 有密码的AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param> 
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
