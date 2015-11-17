using System.Web.Mvc;

namespace Web.Areas.Graduate
{
    public class GraduateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Graduate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Graduate_default",
                "Graduate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
