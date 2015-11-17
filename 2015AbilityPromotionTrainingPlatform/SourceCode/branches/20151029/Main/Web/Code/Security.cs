using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WCS.Code
{
    public static class Security
    {
        public static string HmacSha1Sign(string text, string key)
        {
            Encoding encode = Encoding.UTF8;
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            string k = "";

            k = bytesToHexString(hmac.Hash);
            // return k;
            return Convert.ToBase64String(encode.GetBytes(k));
        }


        private static String bytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder();
            int x = bytes.Count();
            foreach (byte ib in bytes)
            {
                int value = Convert.ToInt32(ib);
                string hexOutput = String.Format("{0:X}", value).PadLeft(2, '0');
                hexString.Append(hexOutput.ToLower());
            }
            return hexString.ToString();
        }
    }
}
