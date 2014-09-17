using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Default.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SelectCities()
        {
            var selectList = new List<SelectListItem>();

            var list = Repository.Cities.ToList();

            foreach (var city in list)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = city.ID.ToString(),
                    Text = city.Name,
                    Selected = city.ID == CurrentCity.ID
                });
            }
            return View(selectList);
        }


        [HttpPost]
        public ActionResult SelectCitiesSelect(int SelectCityID)
        {
            if (CurrentUser != null)
            {
                CurrentUser.CityID = SelectCityID;
                Repository.ChangeUserCity(CurrentUser);
            }
            else
            {
                Session["City"] = SelectCityID;
            }
            return RedirectBack("~/");
        }


        public ActionResult UserLogin()
        {
            return View(CurrentUser);
        }
    }
}