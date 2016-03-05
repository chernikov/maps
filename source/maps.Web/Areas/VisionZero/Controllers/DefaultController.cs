using maps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Routing;

namespace maps.Web.Areas.VisionZero.Controllers
{
    public class DefaultController : BaseController
    {
        protected string DestinationDir = "Content/files/uploads/";

        protected override void Initialize(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo("uk");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;
            base.Initialize(requestContext);
        }
    }
}
