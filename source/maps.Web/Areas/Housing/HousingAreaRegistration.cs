using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace maps.Web.Areas.Housing
{
    public class HousingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Housing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 null,
                 "housing/{controller}/{action}",
                 new { controller = "Home", action = "Index" },
                 new[] { "maps.Web.Areas.Housing.Controllers" }
           );
        }
    }
}
