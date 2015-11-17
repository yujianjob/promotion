﻿using System.Web.Mvc;

namespace Web.Areas.Error
{
    public class ErrorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Error";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Error_default",
                "Error/{controller}/{action}/{id}",
                new { action = "Error", id = UrlParameter.Optional }
            );
        }
    }
}
