using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.Common
{
   public  class Log4Writer
    {
       static Log4Writer me = new Log4Writer("LogFileAppender");
       public static Log4Writer Instance
       {
           get { return me; }
           set { me = value; }
       }

       log4net.ILog log;
       public Log4Writer(string logType)
       {
           log = log4net.LogManager.GetLogger(logType);
       }

       public void Error(string message, Exception ex)
       {
           log.Error(message, ex);
       }
       public void Error(string message)
       {
           log.Error(message);
       }
    }
}
