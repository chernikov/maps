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

namespace maps.Web.Areas.Admin.Controllers
{
    public abstract class AdminController : BaseController
    {
        public readonly int PageSize = 20;
        protected override void Initialize(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo("uk");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;
            base.Initialize(requestContext);
        }

        public override City CurrentCity
        {
            get
            {
                if (Session["City"] != null)
                {
                    var cityID = (int)Session["City"];
                    return Repository.Cities.FirstOrDefault(p => p.ID == cityID);
                }
                return null;
            }

        }

        public City CurrentCityOrDefault
        {
            get
            {
                if (CurrentCity != null)
                {
                    return CurrentCity;
                }
                return DefaultCity;
            }
        }

        public City DefaultCity
        {
            get
            {
                return new City()
                   {
                       CenterLat = 49.0275,
                       CenterLng = 31.482778,
                       Zoom = 6
                   };
            }
        }
    }
}