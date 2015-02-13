using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Bus.Controllers
{
    public class HomeController : BaseBusController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}