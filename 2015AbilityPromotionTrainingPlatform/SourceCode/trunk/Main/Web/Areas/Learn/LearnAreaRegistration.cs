using System.Web.Mvc;

namespace Web.Areas.Learn
{
    public class LearnAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Learn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Learn_default",
                "Learn/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
