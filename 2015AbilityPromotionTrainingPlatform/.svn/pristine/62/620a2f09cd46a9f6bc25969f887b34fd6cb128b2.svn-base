using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web.Areas.Api
{
    public class ApiTools
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        public static void WriteLog(TraceEventType type, int id, string msg)
        {
            string direct = HttpContext.Current.Server.MapPath("~/Log/Api/");
            if (!Directory.Exists(direct))
                Directory.CreateDirectory(direct);
            string file = direct + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            TextWriterTraceListener listen = new TextWriterTraceListener(file);
            listen.TraceOutputOptions = TraceOptions.ProcessId |
                                        TraceOptions.ThreadId |
                                        TraceOptions.Timestamp |
                                        TraceOptions.DateTime |
                                        TraceOptions.LogicalOperationStack;
                                        //TraceOptions.Callstack;

            Trace.Listeners.Add(listen);
            Trace.AutoFlush = false;
            //Trace.WriteLine("Test");

            SourceSwitch sourceSwitch = new SourceSwitch("sourceSwitch");
            sourceSwitch.Level = SourceLevels.Verbose;

            TraceSource traceSource = new TraceSource("traceSource");
            traceSource.Switch = sourceSwitch;
            traceSource.Listeners.Add(listen);

            traceSource.TraceEvent(type, id, msg);
            Trace.Close();
        }

        public static string Encrypt(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}