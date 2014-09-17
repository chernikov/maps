using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using maps.Web.Controllers;
using maps.Model;

namespace maps.Web.Areas.Utility.Controllers
{
    public abstract class UtilityController : BaseController
    {
        public readonly int PageSize = 20;
        protected override void Initialize(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo("uk");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;
            base.Initialize(requestContext);
        }

    }
}