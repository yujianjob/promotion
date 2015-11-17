using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Specialized;
namespace Dianda.Common
{

 
    public class HttpRequestHelper
    { 
#region
        //public string Go()
        //{
        //    WebClient wc = new WebClient();
        //    wc.Encoding = System.Text.Encoding.UTF8;
        //    //wc.Headers.Add(HttpRequestHeader.Accept, "json");
        //    wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded; charset=UTF-8");
        //    wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
        //    string paramStr = "userAccount=drcl1858&pwd=aHVpaGU4MDAz&valid=FB68F3B5FC7CF00F5701CD796F1C8649";

          
        //    wc.Headers.Add("Content-Type", "application/json");
        //    byte[] postData = Encoding.UTF8.GetBytes(paramStr);


        //    byte[] responseData = wc.UploadData("http://61.172.243.7/study/user/index", "POST", postData); // 得到返回字符流
        //    return Encoding.UTF8.GetString(responseData);// 解码              

        //}

        //public string GetResponseData()
        //{
        //    byte[] bytes = Encoding.UTF8.GetBytes(" { 'userAccount': 'drcl1858', 'pwd': 'aHVpaGU4MDAz', 'valid': 'FB68F3B5FC7CF00F5701CD796F1C8649' }");
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://61.172.243.7/study/user/index");  
        //    request.Method = "POST";  
        //request.ContentLength = bytes.Length;  
        //    request.ContentType = "application/json";  
        //    Stream reqstream = request.GetRequestStream();  
        //    reqstream.Write(bytes, 0, bytes.Length);  
  
        //    //声明一个HttpWebRequest请求  
        //    request.Timeout = 90000;  
        //    //设置连接超时时间  
        //    request.Headers.Set("Pragma", "no-cache");  
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();  
        //    Stream streamReceive = response.GetResponseStream();  
        //    Encoding encoding = Encoding.UTF8;  
  
        //    StreamReader streamReader = new StreamReader(streamReceive, encoding);  
        //    string  strResult = streamReader.ReadToEnd();  
        //    streamReceive.Dispose();  
        //    streamReader.Dispose();  
  
        //    return strResult;  
        //}
#endregion
       


        public string GetData(string url, NameValueCollection values,out Exception ex)
        {

            using (WebClient w = new WebClient())
            {
               
                w.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                //System.Collections.Specialized.NameValueCollection  VarPar = new System.Collections.Specialized.NameValueCollection();
                //VarPar.Add("userAccount", "drcl1858");
                //VarPar.Add("pwd", "aHVpaGU4MDAz");
                //VarPar.Add("valid", "FB68F3B5FC7CF00F5701CD796F1C8649");     
                string sRemoteInfo;
                byte[] byRemoteInfo;
                try
                {
                     byRemoteInfo = w.UploadValues(url, "POST", values);
                     sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
                }
                catch(Exception e)
                {
                    ex = e;
                    return string.Empty ;
                }
                ex = null;
                return sRemoteInfo;
            }
        }

        public enum HttpRequestType
        { Get,Post}
      
        public string Read(string url, out Exception ex,Encoding encoding)
        {

            using (WebClient w = new WebClient())
            {
               
                w.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                //System.Collections.Specialized.NameValueCollection  VarPar = new System.Collections.Specialized.NameValueCollection();
                //VarPar.Add("userAccount", "drcl1858");
                //VarPar.Add("pwd", "aHVpaGU4MDAz");
                //VarPar.Add("valid", "FB68F3B5FC7CF00F5701CD796F1C8649");     
                string sRemoteInfo;
              
                try
                {
                    StreamReader sr = new StreamReader(w.OpenRead(url));
                   sRemoteInfo=  sr.ReadToEnd();
                   // byRemoteInfo = w.UploadValues(url, type.ToString(), values);
                   // sRemoteInfo = encoding.GetString(byRemoteInfo);
                }
               
                catch (Exception e)
                {
                    ex = e;
                    return string.Empty;
                }
                ex = null;
                return sRemoteInfo;
            }
        }
    }
}
