using System.Web.Mvc;

namespace Web.Areas.Course
{
    public class CourseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Course";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Course_default",
                "Course/{controller}/{action}/{id}",
                new { action = "CourseList", id = UrlParameter.Optional }
            );
        }
    }
}
