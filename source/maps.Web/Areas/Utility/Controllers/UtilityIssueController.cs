using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using maps.Model;
using maps.Web.Models.ViewModels;

namespace maps.Web.Areas.Utility.Controllers
{
    public class UtilityIssueController : UtilityController
    {
        public ActionResult Index(int page = 1, int? status = null)
        {
            var list = Repository.UtilityIssues;
            if (status.HasValue)
            {
                list = list.Where(p => p.Status == status.Value);
            }
            var orderedList = list.OrderByDescending(p => p.ID);
            ViewBag.Status = status;
            return View(orderedList.ToPagedList(page, PageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new UtilityIssueView()
            {
                City = (CityView)ModelMapper.Map(CurrentCity, typeof(City), typeof(CityView))
            });
        }

        public ActionResult Print(int id)
        {
            var item = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                if (item.Status == (int)UtilityIssue.StatusType.Create)
                {
                    Repository.AcceptUtilityIssue(item, CurrentUser.ID);
                }
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);

            if (utilityIssue != null)
            {
                var utilityIssueView = (UtilityIssueView)ModelMapper.Map(utilityIssue, typeof(UtilityIssue), typeof(UtilityIssueView));
                return View(utilityIssueView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(UtilityIssueView utilityIssueView)
        {
            if (ModelState.IsValid)
            {
                var utilityIssue = (UtilityIssue)ModelMapper.Map(utilityIssueView, typeof(UtilityIssueView), typeof(UtilityIssue));

                if (!string.IsNullOrWhiteSpace(utilityIssueView.UtilityDepartmentName))
                {
                    var department = Repository.UtilityDepartments.FirstOrDefault(p =>
                        string.Compare(p.Name, utilityIssueView.UtilityDepartmentName, true) == 0);
                    if (department == null)
                    {
                        department = new UtilityDepartment()
                        {
                            Name = utilityIssueView.UtilityDepartmentName,
                            CityID = CurrentCity.ID
                        };
                        Repository.CreateUtilityDepartment(department);
                    }
                    utilityIssue.UtilityDepartmentID = department.ID;
                }
                if (utilityIssue.ID == 0)
                {
                    utilityIssue.UserID = CurrentUser.ID;
                    utilityIssue.CityID = CurrentCity.ID;
                    Repository.CreateUtilityIssue(utilityIssue);
                }
                else
                {
                    Repository.UpdateUtilityIssue(utilityIssue, CurrentUser.ID);
                }
                Repository.ClearAllUtilityIssueTags(utilityIssueView.ID);
                if (utilityIssueView.UtilityTagList != null)
                {
                    foreach (var tagId in utilityIssueView.UtilityTagList)
                    {
                        var tag = Repository.UtilityTags.FirstOrDefault(p => p.ID == tagId);
                        if (tag != null)
                        {
                            Repository.CreateUtilityIssueTag(new UtilityIssueTag()
                            {
                                UtilityTagID = tag.ID,
                                UtilityIssueID = utilityIssue.ID
                            });
                        }
                    }
                }

                Repository.ClearAllUtilityIssuePhotos(utilityIssueView.ID);
                if (utilityIssueView.Photos != null)
                {
                    foreach (var utilityPhotoView in utilityIssueView.Photos)
                    {
                        var utilityPhoto = Repository.UtilityPhotos.FirstOrDefault(p => p.ID == utilityPhotoView.Value.ID);
                        if (utilityPhoto != null)
                        {
                            utilityPhoto.UserID = CurrentUser.ID;
                            utilityPhoto.UtilityIssueID = utilityIssue.ID;
                            utilityPhoto.IsRemoved = false;
                            utilityPhoto.State = utilityPhotoView.Value.State;
                            Repository.UpdateUtilityPhoto(utilityPhoto);
                        }
                    }
                }


                return RedirectToAction("Index");
            }
            return View(utilityIssueView);
        }

        public ActionResult Delete(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (utilityIssue != null)
            {
                Repository.RemoveUtilityIssue(utilityIssue.ID, CurrentUser.ID);
            }
            return RedirectBack("Index");
        }

        public ActionResult Accept(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (utilityIssue != null)
            {
                Repository.AcceptUtilityIssue(utilityIssue, CurrentUser.ID);
            }
            return RedirectBack("Index");
        }

        public ActionResult ReAccept(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (utilityIssue != null)
            {
                Repository.AcceptBackUtilityIssue(utilityIssue, CurrentUser.ID);
            }
            return RedirectBack("Index");
        }


        public ActionResult Resolve(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (utilityIssue != null)
            {
                Repository.ResolveUtilityIssue(utilityIssue, CurrentUser.ID);
            }
            return RedirectBack("Index");
        }

        public ActionResult Close(int id)
        {
            var utilityIssue = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (utilityIssue != null)
            {
                Repository.CloseUtilityIssue(utilityIssue, CurrentUser.ID);
            }
            return RedirectBack("Index");
        }
    }
}