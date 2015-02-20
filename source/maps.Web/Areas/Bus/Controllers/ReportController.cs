using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Bus.Controllers
{
    [Authorize]
    public class ReportController : BaseBusController
    {
        public ActionResult Create()
        {
            return View(new NewReportView()
            {
                UserID = CurrentUser.ID
            });
        }

        public ActionResult Create(NewReportView newReportView)
        {
            return RedirectToAction("Index");
        }

        public ActionResult SelectBus(int? routeId)
        {
            if (routeId.HasValue)
            {

            }
            return null;
        }
    }
}