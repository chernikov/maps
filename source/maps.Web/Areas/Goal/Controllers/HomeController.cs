using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;
using Tool;

namespace maps.Web.Areas.Goal.Controllers
{
    public class HomeController : GoalController
    {
        
        //
        // GET: /Goal/Home/
        public ActionResult Index(string url = null)
        {
            ViewBag.Url = url;
            return View();
        }

        public ActionResult Item(string url = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                if (CurrentUser != null) {
                    var item = Repository.Goals.FirstOrDefault(p => p.UserID == CurrentUser.ID);
                    if (item == null)
                    {
                        return View("Create", new GoalView());
                    }
                    return View(item);
                }
            }
            else
            {
                var item = Repository.Goals.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0 && p.IsPublic);
                if (item != null)
                {
                    return View(item);
                }
            }
            return View("Item", null);
        }

        [HttpPost]
        public ActionResult Create(GoalView goalView)
        {
            if (ModelState.IsValid)
            {
                var goal = (Model.Goal)ModelMapper.Map(goalView, typeof(GoalView), typeof(Model.Goal));
                goal.UserID = CurrentUser.ID;
                goal.Url = string.Format("{0}-{1}", Translit.Translate(CurrentUser.LastName), Translit.Translate(goal.Text));
                Repository.CreateGoal(goal);
                return View("_Ok");
            }
            return View(goalView);
        }

        public ActionResult Set(int id)
        {
            var goalCell = Repository.GoalCells.FirstOrDefault(p => p.ID == id);
            if (goalCell != null && goalCell.Goal.UserID == CurrentUser.ID)
            {
                if (goalCell.State == 0)
                {
                    goalCell.State = 1;
                    goalCell.AddedDate = DateTime.Now;
                    Repository.UpdateGoalCell(goalCell);
                    var goal = Repository.Goals.FirstOrDefault(p => p.ID == goalCell.GoalID);
                    return Json(new { result = "ok", value = "set", count = goal.Count, progress = goal.Progress }, JsonRequestBehavior.AllowGet);
                }
                if (goalCell.State == 1 && goalCell.AddedDate.HasValue && goalCell.AddedDate.Value.AddDays(1) > DateTime.Now)
                {
                    goalCell.State = 0;
                    goalCell.AddedDate =null;
                    Repository.UpdateGoalCell(goalCell);
                    var goal = Repository.Goals.FirstOrDefault(p => p.ID == goalCell.GoalID);
                    return Json(new { result = "ok", value = "empty", count = goal.Count, progress = goal.Progress }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = "none" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reset(int id)
        {
            var goal = Repository.Goals.FirstOrDefault(p => p.ID == id);
            if (goal != null && goal.UserID == CurrentUser.ID)
            {
                Repository.ClearAllCells(goal.ID);
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var goal = Repository.Goals.FirstOrDefault(p => p.ID == id);
            if (goal != null && goal.UserID == CurrentUser.ID)
            {
                Repository.RemoveGoal(goal.ID);
            }
            return RedirectToAction("Index");
        }
	}
}