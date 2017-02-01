using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace maps.Web.Areas.VisionZero
{
    public class VisionZeroAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "VisionZero";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                null,
                "vision-zero/geo",
                new { controller = "Geocoding", action = "Index" },
                new[] { "maps.Web.Areas.VisionZero.Controllers" }
            );

            context.MapRoute(
                null,
                "vision-zero/shared/{id}",
                new { controller = "Home", action = "Shared" },
                new[] { "maps.Web.Areas.VisionZero.Controllers" }
            );

            context.MapRoute(
                "VisionZero_default",
                "vision-zero/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.VisionZero.Controllers" }
            );
        }

    }
}
