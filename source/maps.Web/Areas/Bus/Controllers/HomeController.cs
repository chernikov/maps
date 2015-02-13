using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Bus.Controllers
{
    public class AdminController : BaseBusController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}