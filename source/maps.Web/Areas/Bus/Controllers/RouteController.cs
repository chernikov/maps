using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Bus.Controllers
{
    public class RouteController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.Routes.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new RouteView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var route = Repository.Routes.FirstOrDefault(p => p.ID == id);

            if (route != null)
            {
                var routeView = (RouteView)ModelMapper.Map(route, typeof(Route), typeof(RouteView));
                return View(routeView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(RouteView routeView)
        {
            if (ModelState.IsValid)
            {
                var route = (Route)ModelMapper.Map(routeView, typeof(RouteView), typeof(Route));
                if (route.ID == 0)
                {
                    Repository.CreateRoute(route);
                }
                else
                {
                    Repository.UpdateRoute(route);
                }
                return RedirectToAction("Index");
            }
            return View(routeView);
        }

        public ActionResult Delete(int id)
        {
            var route = Repository.Routes.FirstOrDefault(p => p.ID == id);
            if (route != null)
            {
                Repository.RemoveRoute(route.ID);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditBatch()
        {
            return View("EditBatch", string.Empty);
        }

        [HttpPost]
        public ActionResult EditBatch(string Data)
        {
            var lines = Data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var route = new Route()
                {
                    Name = line.Trim(),
                };
                Repository.CreateRoute(route);
           }
           return View("EditBatch", string.Empty);
        }
    }
}