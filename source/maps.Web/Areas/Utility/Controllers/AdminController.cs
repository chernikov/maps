using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Utility.Controllers
{
    [Authorize(Roles = "admin,utility_moderator")]
    public class AdminController : UtilityController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}