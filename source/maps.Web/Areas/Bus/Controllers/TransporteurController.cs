using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;

namespace maps.Web.Areas.Bus.Controllers
{
    [Authorize(Roles="admin")]
    public class TransporteurController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.Transporteurs.ToList();
            return View(list);
        }


        public ActionResult Create()
        {
            return View("Edit", new TransporteurView());
        }

        [HttpGet]
        public ActionResult EditBatch()
        {
            return View("EditBatch", string.Empty);
        }

        [HttpPost]
        public ActionResult EditBatch(string Data)
        {
            var lines = Data.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach(var line in lines) 
            {
                var columns = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                if (columns.Count() >= 4)
                {
                    var transporteur = new Transporteur()
                    {
                        LastName = columns[0],
                        FirstName = columns[1],
                        Patronymic = columns[2],
                        PrimaryPhone = columns[3]
                    };
                    Repository.CreateTransporteur(transporteur);
                }
            }
            return View("EditBatch", Data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transporteur = Repository.Transporteurs.FirstOrDefault(p => p.ID == id);

            if (transporteur != null)
            {
                var transporteurView = (TransporteurView)ModelMapper.Map(transporteur, typeof(Transporteur), typeof(TransporteurView));
                return View(transporteurView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(TransporteurView transporteurView)
        {
            if (ModelState.IsValid)
            {
                var transporteur = (Transporteur)ModelMapper.Map(transporteurView, typeof(TransporteurView), typeof(Transporteur));
                if (transporteur.ID == 0)
                {
                    Repository.CreateTransporteur(transporteur);
                }
                else
                {
                    Repository.UpdateTransporteur(transporteur);
                }
                return RedirectToAction("Index");
            }
            return View(transporteurView);
        }

        public ActionResult Delete(int id)
        {
            var transporteur = Repository.Transporteurs.FirstOrDefault(p => p.ID == id);
            if (transporteur != null)
            {
                Repository.RemoveTransporteur(transporteur.ID);
            }
            return RedirectToAction("Index");
        }
    }
}