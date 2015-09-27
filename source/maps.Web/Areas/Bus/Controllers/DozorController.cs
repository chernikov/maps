using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Dozor;

namespace maps.Web.Areas.Bus.Controllers
{
    public class DozorController : Controller
    {
        // GET: Bus/Dozor
        public ActionResult Index()
        {
            var dozor = new DataCollector();
            //var responce = dozor.GetData();
            //return Content(responce);
            return null;
        }
    }
}