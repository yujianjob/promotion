using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.Common
{
   public  class TimeHelper
    {
       public static string FromNowTime(DateTime current)
       {
           return FromNowTime(current, DateTime.Now);
       }
       public static string FromNowTime(DateTime current, DateTime now)
       {
           TimeSpan ts = now - current;
           if (ts.TotalSeconds < 60)
           {           
               return  "1分钟前";
           }
           else if (ts.TotalMinutes < 60)
               return ts.TotalMinutes.ToString("##") + "分钟前";
           else if (ts.TotalHours < 24)
           {
               return ts.TotalHours.ToString("##") + "小时前";
           }
           else if (ts.TotalDays < 30)
           {
               return ts.TotalDays.ToString("##") + "天前";
           }
           else if (ts.TotalDays < 365)
           {
               return (ts.TotalDays / 30).ToString("##") + "月前";
           }
           else
           {
               return (ts.TotalDays / 365).ToString("##") + "年前";
           }
       }
    }
}
