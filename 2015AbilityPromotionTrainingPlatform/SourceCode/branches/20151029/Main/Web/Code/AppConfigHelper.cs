using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code
{
    public class AppConfigHelper
    {
        static AppConfigHelper me = new AppConfigHelper();
        public static AppConfigHelper Instance
        {
            get { return me; }
            set { me = value; }
        }
        bool isTest = System.Configuration.ConfigurationManager.AppSettings["IsTest"]=="1";
        public bool IsTest
        {
            get { return isTest; }
          private  set{isTest=value;}
        }
    }
}