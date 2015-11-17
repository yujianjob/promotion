using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Attributes
{
    public class ExceptionLogAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string direct = filterContext.HttpContext.Server.MapPath("~/Log/");
            if (!Directory.Exists(direct))
                Directory.CreateDirectory(direct);
            string file = direct + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string content = DateTime.Now + " " + new string('-', 100) + "\r\n"
                + filterContext.Exception.Message + "\r\n";
            File.AppendAllText(file, content, Encoding.UTF8);
        }
    }
}