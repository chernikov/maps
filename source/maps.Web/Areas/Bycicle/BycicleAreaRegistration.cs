using System.Web.Mvc;

namespace maps.Web.Areas.Bycicle
{
    public class BycicleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Bycicle";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Bycicle_default",
                "bycicle/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Bycicle.Controllers" }
            );
        }
    }
}