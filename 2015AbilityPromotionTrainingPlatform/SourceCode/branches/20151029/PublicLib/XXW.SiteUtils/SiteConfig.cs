using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace XXW.SiteUtils
{
    public class SiteConfig
    {
        public string LocalHost4StyleSheet { get; set; }
        public string LocalHost4Script { get; set; }
        public string CDN4StyleSheet { get; set; }
        public string CDN4Script { get; set; }

        public string RequestContentTmpl4Friend { get; set; }
        public string ResponseContentTmpl4Friend { get; set; }

        public string RequestContentTmpl4Circle { get; set; }
        public string ResponseContentTmpl4Circle { get; set; }
        public string ConfirmSuffix4Circle { get; set; }

        public string RequestContentTmpl4Msg { get; set; }
        public string ResponseContentTmpl4Msg { get; set; }

        public string ContentTmpl4System { get; set; }

        private static string STYLE_SHEET_NODE_NAME = "styleSheet";
        private static string SCRIPT_NODE_NAME = "script";
        private static string REQUEST_NODE_NAME = "request";
        private static string RESPONSE_NODE_NAME = "response";

        private static SiteConfig _instance;
        public static SiteConfig Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                XmlDocument doc = LoadXML();
                _instance = new SiteConfig();
                XmlNode n;
                XmlNode hostNode = doc.GetElementsByTagName("host")[0];
                n = hostNode.SelectNodes("local")[0];
                _instance.LocalHost4StyleSheet = n.SelectNodes(STYLE_SHEET_NODE_NAME)[0].FirstChild.Value;
                _instance.LocalHost4Script = n.SelectNodes(SCRIPT_NODE_NAME)[0].FirstChild.Value;
                n = hostNode.SelectNodes("cdn")[0];
                _instance.CDN4StyleSheet = n.SelectNodes(STYLE_SHEET_NODE_NAME)[0].FirstChild.Value;
                _instance.CDN4Script = n.SelectNodes(SCRIPT_NODE_NAME)[0].FirstChild.Value;

                XmlNode infoNode = doc.GetElementsByTagName("infoContent")[0];
                n = infoNode.SelectNodes("friend")[0];
                _instance.RequestContentTmpl4Friend = n.SelectNodes(REQUEST_NODE_NAME)[0].FirstChild.Value;
                _instance.ResponseContentTmpl4Friend = n.SelectNodes(RESPONSE_NODE_NAME)[0].FirstChild.Value;

                //n = infoNode.SelectNodes("circle")[0];
                //_instance.RequestContentTmpl4Circle = n.SelectNodes(REQUEST_NODE_NAME)[0].FirstChild.Value;
                //_instance.ResponseContentTmpl4Circle = n.SelectNodes(RESPONSE_NODE_NAME)[0].FirstChild.Value;
                //_instance.ConfirmSuffix4Circle = n.Attributes["confirmSuffix"].Value;

                //n = infoNode.SelectNodes("msg")[0];
                //_instance.RequestContentTmpl4Msg = n.SelectNodes(REQUEST_NODE_NAME)[0].FirstChild.Value;
                //_instance.ResponseContentTmpl4Msg = n.SelectNodes(RESPONSE_NODE_NAME)[0].FirstChild.Value;

                //n = infoNode.SelectNodes("system")[0];
                //_instance.ContentTmpl4System = n.FirstChild.Value;

                return _instance;
            }
        }

        private static XmlDocument LoadXML()
        {
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("/bin/config/site.config"));
            return doc;
        }
    }
}
