using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Bus.Controllers
{
    public class BrandController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.Brands.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new BrandView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var brand = Repository.Brands.FirstOrDefault(p => p.ID == id);

            if (brand != null)
            {
                var brandView = (BrandView)ModelMapper.Map(brand, typeof(Brand), typeof(BrandView));
                return View(brandView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(BrandView brandView)
        {
            if (ModelState.IsValid)
            {
                var brand = (Brand)ModelMapper.Map(brandView, typeof(BrandView), typeof(Brand));
                if (brand.ID == 0)
                {
                    Repository.CreateBrand(brand);
                }
                else
                {
                    Repository.UpdateBrand(brand);
                }
                return RedirectToAction("Index");
            }
            return View(brandView);
        }

        public ActionResult Delete(int id)
        {
            var brand = Repository.Brands.FirstOrDefault(p => p.ID == id);
            if (brand != null)
            {
                Repository.RemoveBrand(brand.ID);
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
                var brand = new Brand()
                {
                    Name = line.Trim(),
                };
                Repository.CreateBrand(brand);
           }
           return View("EditBatch", string.Empty);
        }
    }
}