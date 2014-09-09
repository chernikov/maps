using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class HomeController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SelectCities()
        {
            var selectList = new List<SelectListItem>();

            var list = Repository.Cities.ToList();
            selectList.Add(new SelectListItem()
            {
                Value = "",
                Text = "Усі",
                Selected = CurrentCity == null
            });
            foreach (var city in list)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = city.ID.ToString(),
                    Text = city.Name,
                    Selected = CurrentCity != null && city.ID == CurrentCity.ID
                });
            }
            return View(selectList);
        }

       


        public ActionResult SelectCities(int SelectCityID)
        {
            Session["City"] = SelectCityID;
            return RedirectBack("~/");
        }
    }
}