using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace maps.Web.Areas.Bus.Controllers
{
    public class HomeController : BaseBusController
    {
        private const int PageSize = 20;
        public ActionResult Index(int page = 1)
        {
            var reports = Repository.Reports.OrderByDescending(p => p.ID);

            return View(reports.ToPagedList(page, PageSize));
        }

        public ActionResult Info()
        {
            return View();
        }
    }
}