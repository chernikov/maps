using System.Web.Mvc;

namespace maps.Web.Areas.Sport
{
    public class SportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sport_default",
                "illlegal/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Sport.Controllers" }
            );
        }
    }
}