using System.Web.Mvc;

namespace Web.Areas.Prepare
{
    public class PrepareAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Prepare";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Prepare_default",
                "Prepare/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
