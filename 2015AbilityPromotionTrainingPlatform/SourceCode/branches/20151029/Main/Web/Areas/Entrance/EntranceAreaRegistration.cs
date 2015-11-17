using System.Web.Mvc;

namespace Web.Areas.Entrance
{
    public class EntranceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Entrance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Entrance_default",
                "Entrance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
