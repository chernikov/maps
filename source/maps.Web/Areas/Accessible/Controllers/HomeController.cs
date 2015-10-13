using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Accessible.Controllers
{
    public class HomeController : DefaultController
    {
        // GET: Accessible/Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Object(int id)
        {
            var item = Repository.AccessibleObjects.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Place(int id)
        {
            var item = Repository.AccessiblePlaces.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Index");
        }
    }
}