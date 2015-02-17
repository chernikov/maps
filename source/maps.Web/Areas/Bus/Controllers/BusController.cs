using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Bus.Controllers
{
    public class BusController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.Bus.OrderBy(p => p.Number).ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new BusView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bus = Repository.Bus.FirstOrDefault(p => p.ID == id);

            if (bus != null)
            {
                var busView = (BusView)ModelMapper.Map(bus, typeof(Model.Bus), typeof(BusView));
                return View(busView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(BusView busView)
        {
            if (ModelState.IsValid)
            {
                var bus = (Model.Bus)ModelMapper.Map(busView, typeof(BusView), typeof(Model.Bus));
                if (bus.ID == 0)
                {
                    Repository.CreateBus(bus);
                }
                else
                {
                    Repository.UpdateBus(bus);
                }
                return RedirectToAction("Index");
            }
            return View(busView);
        }

        public ActionResult Delete(int id)
        {
            var bus = Repository.Bus.FirstOrDefault(p => p.ID == id);
            if (bus != null)
            {
                Repository.RemoveBus(bus.ID);
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
            var routes = Repository.Routes.ToList();
            var transporteurs = Repository.Transporteurs.ToList();
            var brands = Repository.Brands.ToList();

            var errors = new List<string>();

            var lines = Data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var items = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                var route = routes.FirstOrDefault(p => p.Name.Trim() == items[0].Trim());
                var brand = brands.FirstOrDefault(p => p.Name.Trim() == items[2].Trim());
                var transporteur = transporteurs.FirstOrDefault(p => p.FullName.Trim() == items[3].Trim());
                if (route != null && brand != null && transporteur != null)
                {
                    var bus = new Model.Bus()
                    {
                        RouteID = route.ID,
                        BrandID = brand.ID,
                        TransporteurID = transporteur.ID,
                        Number = items[1]
                    };
                    Repository.CreateBus(bus);
                }
                else
                {
                    errors.Add(line);
                }
           }
            ViewBag.Errors = errors;
           return View("EditBatch", string.Empty);
        }
    }
}