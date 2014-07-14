using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class HomeController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}