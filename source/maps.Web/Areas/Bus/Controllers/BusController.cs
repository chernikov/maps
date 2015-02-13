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
            var list = Repository.Bus.ToList();
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
            var lines = Data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            /*foreach (var line in lines)
            {
                var bus = new Model.Bus()
                {
                    Name = line.Trim(),
                };
                Repository.CreateBus(bus);
           }*/
           return View("EditBatch", string.Empty);
        }
    }
}