using maps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace maps.Web.Areas.Accessible.Controllers
{
    public class DefaultController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo("uk");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;
            base.Initialize(requestContext);
        }
    }
}