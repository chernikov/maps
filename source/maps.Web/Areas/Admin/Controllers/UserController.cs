using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;
using maps.Web.Models.ViewModels.User;
using PagedList;

namespace maps.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Users.OrderBy(p => p.ID);
            return View(list.ToPagedList(page, PageSize));
        }

        public ActionResult Create()
        {
            var userView = new UserView();
            return View("Edit", userView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == id);

            if (user != null)
            {
                var userView = (UserView)ModelMapper.Map(user, typeof(User), typeof(UserView));
                return View(userView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(UserView userView)
        {
            if (ModelState.IsValid)
            {
                var user = (User)ModelMapper.Map(userView, typeof(UserView), typeof(User));
                if (user.ID == 0)
                {
                    Repository.CreateUser(user);
                }
                else
                {
                    Repository.UpdateUser(user);
                }
                return RedirectToAction("Index");
            }
            return View(userView);
        }
    }
}