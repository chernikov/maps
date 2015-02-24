using System.Web.Mvc;

namespace maps.Web.Areas.Bus
{
    public class BusAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Bus";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                  null,
                  "report/{id}",
                  new { controller = "Report", action = "Item" },
                  new[] { "maps.Web.Areas.Bus.Controllers" }
            );
        
            context.MapRoute(
                "Bus_default",
                "bus/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Bus.Controllers" }
            );
        }
    }
}