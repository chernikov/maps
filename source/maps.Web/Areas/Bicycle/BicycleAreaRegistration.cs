using System.Web.Mvc;

namespace maps.Web.Areas.Bicycle
{
    public class BicycleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Bicycle";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Bicycle_default",
                "bicycle/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Bicycle.Controllers" }
            );
        }
    }
}