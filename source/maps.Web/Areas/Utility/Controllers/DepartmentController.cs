using maps.Model;
using maps.Web.Global.Search;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Utility.Controllers
{
    public class DepartmentController : UtilityController
    {
        public ActionResult Index()
        {
            var list = Repository.UtilityDepartments.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new UtilityDepartmentView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var utilityDepartment = Repository.UtilityDepartments.FirstOrDefault(p => p.ID == id);

            if (utilityDepartment != null)
            {
                var utilityDepartmentView = (UtilityDepartmentView)ModelMapper.Map(utilityDepartment, typeof(UtilityDepartment), typeof(UtilityDepartmentView));
                return View(utilityDepartmentView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(UtilityDepartmentView utilityDepartmentView)
        {
            if (ModelState.IsValid)
            {
                var utilityDepartment = (UtilityDepartment)ModelMapper.Map(utilityDepartmentView, typeof(UtilityDepartmentView), typeof(UtilityDepartment));
                if (utilityDepartment.ID == 0)
                {
                    utilityDepartment.CityID = CurrentCity.ID;
                    Repository.CreateUtilityDepartment(utilityDepartment);
                }
                else
                {
                    Repository.UpdateUtilityDepartment(utilityDepartment);
                }
                return RedirectToAction("Index");
            }
            return View(utilityDepartmentView);
        }

        public ActionResult Delete(int id)
        {
            var utilityDepartment = Repository.UtilityDepartments.FirstOrDefault(p => p.ID == id);
            if (utilityDepartment != null)
            {
                Repository.RemoveUtilityDepartment(utilityDepartment.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Get(string str)
        {
            var list = Repository.UtilityDepartments;

            var searchList = SearchEngine.Get(str, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }
	}
}