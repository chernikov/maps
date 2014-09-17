using maps.Model;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Utility.Controllers
{
    public class TagController : UtilityController
    {
        public ActionResult Index()
        {
            var list = Repository.UtilityTags.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new UtilityTagView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var utilityTag = Repository.UtilityTags.FirstOrDefault(p => p.ID == id);

            if (utilityTag != null)
            {
                var utilityTagView = (UtilityTagView)ModelMapper.Map(utilityTag, typeof(UtilityTag), typeof(UtilityTagView));
                return View(utilityTagView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(UtilityTagView utilityTagView)
        {
            if (ModelState.IsValid)
            {
                var utilityTag = (UtilityTag)ModelMapper.Map(utilityTagView, typeof(UtilityTagView), typeof(UtilityTag));
                if (utilityTag.ID == 0)
                {
                    Repository.CreateUtilityTag(utilityTag);
                }
                else
                {
                    Repository.UpdateUtilityTag(utilityTag);
                }
                return RedirectToAction("Index");
            }
            return View(utilityTagView);
        }

        public ActionResult Delete(int id)
        {
            var utilityTag = Repository.UtilityTags.FirstOrDefault(p => p.ID == id);
            if (utilityTag != null)
            {
                Repository.RemoveUtilityTag(utilityTag.ID);
            }
            return RedirectToAction("Index");
        }
	}
}