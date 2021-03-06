﻿using ImageResizer;
using maps.Model;
using maps.Web.Global;
using maps.Web.Models.ViewModels;
using maps.Web.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;

namespace maps.Web.Areas.Bus.Controllers
{

    public class ReportController : BaseBusController
    {
        [HttpGet]
        public ActionResult New()
        {
            if (CurrentUser != null)
            {
                return RedirectToAction("Create", "Report");
            }
            return View(new NewReportView()
            {
                DateTime = DateTime.Now.Date.AddHours(12)
            });
        }



        [HttpPost]
        public ActionResult New(NewReportView newReportView)
        {
            if (ModelState.IsValid)
            {
                if (CurrentUser == null) 
                {
                    newReportView.Mobile = "38" + newReportView.Mobile.ClearPhone();         
                    var savedCode = Session["Code"] as string;
                    var savedMobile = Session["Mobile"] as string;
                    if (newReportView.Code == savedCode && newReportView.Mobile == savedMobile)
                    {
                        var user = Repository.Users.FirstOrDefault(p => p.Mobile == newReportView.Mobile);
                        if (user == null)
                        {
                            var newUser = new User()
                            {
                                Login = Translit.Translate(string.Format("{0}_{1}", newReportView.LastName, newReportView.FirstName)),
                                LastName = newReportView.LastName,
                                FirstName = newReportView.FirstName,
                                Mobile = newReportView.Mobile,
                                Password = newReportView.Code, 
                                CityID = CurrentCity.ID
                            };
                            Repository.CreateUser(newUser);
                            newReportView.UserID = newUser.ID;
                            Auth.Login(newUser.Login);
                        }
                        else
                        {
                            newReportView.UserID = user.ID;
                            Auth.Login(user.Login);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Code", "Код введено невірно");
                    }
                }
                CreateReport(newReportView);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateReportView()
            {
                UserID = CurrentUser.ID,
                DateTime = DateTime.Now.Date.AddHours(12)
            });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateReportView createReportView)
        {
            if (ModelState.IsValid)
            {
                createReportView.UserID = CurrentUser.ID;

                CreateReport(createReportView);
            }
            return RedirectToAction("Index", "Home");
        }

        private void CreateReport(CreateReportView createReportView)
        {
            var report = (Report)ModelMapper.Map(createReportView, typeof(CreateReportView), typeof(Report));
            var rules = Repository.Rules.Where(p => createReportView.SelectedRules.Contains(p.ID));
            report.Type = rules.Any(p => p.IsRouteScope) ? (int)Report.TypeEnum.RouteScope : (int)Report.TypeEnum.BusScope;
            report.Status = (int)Report.StatusEnum.New;
            Repository.CreateReport(report);

            if (createReportView.Photos != null)
            {
                foreach (var reportPhotoView in createReportView.Photos)
                {
                    var reportPhoto = Repository.ReportPhotos.FirstOrDefault(p => p.ID == reportPhotoView.Value.ID);
                    if (reportPhoto != null)
                    {
                        reportPhoto.ReportID = report.ID;
                        Repository.UpdateReportPhoto(reportPhoto);
                    }
                }
            }
            if (createReportView.SelectedRules != null)
            {
                foreach (var ruleId in createReportView.SelectedRules)
                {
                    Repository.CreateRuleReport(new RuleReport()
                    {
                        ReportID = report.ID,
                        RuleID = ruleId
                    });
                }
            }
        }

        public ActionResult SelectBus(int? routeId, int? busId = null)
        {
            var buses = Repository.Bus;
            if (routeId.HasValue)
            {
                buses = buses.Where(p => p.RouteID == routeId);
            }
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Value = "",
                Text = "Не вибраний",
                Selected = !busId.HasValue
            });

            foreach (var bus in buses)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = bus.ID.ToString(),
                    Text = string.Format("{0} (#{1}) {2}", bus.Number, bus.Route.Name, bus.Brand.Name),
                    Selected = busId == bus.ID
                });
            }
            return View(selectList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult Upload(FineUpload upload)
        {
            var uDir = "Content/files/utilities/";
            var extension = Path.GetExtension(upload.Filename);
            var uFile = StringExtension.GenerateNewFile() + extension;
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);
            try
            {
                ImageBuilder.Current.Build(upload.InputStream, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
                var reportPhoto = new ReportPhoto()
                {
                    ImagePath = "/" + uDir + uFile,
                };
                Repository.CreateReportPhoto(reportPhoto);
                return new FineUploaderResult(true, new { reportPhoto });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
        }

        public ActionResult Photo(int id)
        {
            var reportPhoto = Repository.ReportPhotos.FirstOrDefault(p => p.ID == id);
            if (reportPhoto != null)
            {
                var reportPhotoView = (ReportPhotoView)ModelMapper.Map(reportPhoto, typeof(ReportPhoto), typeof(ReportPhotoView));
                return View("ReportPhoto", new KeyValuePair<string, ReportPhotoView>(Guid.NewGuid().ToString("N"), reportPhotoView));
            }
            return null;
        }

        public ActionResult Item(int id)
        {
            var report = Repository.Reports.FirstOrDefault(p => p.ID == id);

            if (report == null)
            {
                return RedirectToNotFoundPage;
            }

            return View(report);
        }

        public ActionResult SendSms(int id)
        {
            var report = Repository.Reports.FirstOrDefault(p => p.ID == id);
            if (report.Bus != null)
            {
                report.Status = (int)Report.StatusEnum.Notified;
                Repository.UpdateReport(report);
                var transporteur = report.Bus.Transporteur;
                var result = SmsSender.SendSms("38" + transporteur.PrimaryPhone.ClearPhone(),
                    string.Format("Porushennja! Bus " + report.Bus.Number + ". Bil'she: http://maps.if.ua/report/" + report.ID));

                Repository.CreateNotify(new Notify()
                {
                    ReportID = report.ID,
                    Phone = "38" + transporteur.PrimaryPhone.ClearPhone(),
                    Result = result,
                    Text = string.Format("Porushennja! Bus " + report.Bus.Number + ". Bil'she: http://maps.if.ua/report/" + report.ID),
                    Sender = "IFBus"
                });
            }

            return RedirectToAction("Item", new { id });
        }

        public ActionResult SendCode(string mobile)
        {
            mobile = "38" + mobile.ClearPhone();
            if (mobile.IsPhone())
            {
                var user = Repository.Users.FirstOrDefault(p => p.Mobile == mobile);
                if (user != null) 
                {
                    var result = SmsSender.SendSms(user.Mobile, "Vash parol': " + user.Password);
                    Repository.CreateNotify(new Notify()
                    {
                        ReportID = null,
                        Phone = user.Mobile,
                        Result = result,
                        Text = "Vash parol': " + user.Password,
                        Sender = "IFBus"
                    });

                    return Json(new { result = "ok", data = result }, JsonRequestBehavior.AllowGet);
                } else {
                    var code = StringExtension.CreateRandomPassword(6, "0123456789");
                    Session["Mobile"] = mobile;
                    Session["Code"] = code;
                    var result = SmsSender.SendSms(mobile, "Vash cod: " + code);
                    Repository.CreateNotify(new Notify()
                    {
                        ReportID = null,
                        Phone = mobile,
                        Result = result,
                        Text = "Vash cod: " + code,
                        Sender = "IFBus"
                    });
                    return Json(new { result = "ok", data = result }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = "error", data = "Введіть мобільний телефон" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckCode(string mobile, string code)
        {
             mobile = "38" + mobile.ClearPhone();
            var user = Repository.Users.FirstOrDefault(p => p.Mobile == mobile);
            if (user != null)
            {
                if (code == user.Password)
                {
                    Auth.Login(user.Login);
                    return Json(new { result = "ok", userID = user.ID }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var savedCode = Session["Code"] as string;
                var savedMobile = Session["Mobile"] as string;
                if (code == savedCode && mobile == savedMobile)
                {
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = "error", data = "Код не вірний" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Answer(int id)
        {
            var report = Repository.Reports.FirstOrDefault(p => p.ID == id);
            if (report != null)
            {
                if (CurrentUser == null || !CurrentUser.InRoles("transporteur") || !report.HasAccess(CurrentUser))
                {
                    return RedirectToAction("Index", "Login", new { id });
                }
                var answerReportView = new ReportAnswerView()
                {
                    ReportID = report.ID
                };
                return View(answerReportView);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Answer(ReportAnswerView reportAnswerView)
        {
            if (ModelState.IsValid)
            {
                var report = Repository.Reports.FirstOrDefault(p => p.ID == reportAnswerView.ReportID);
                if (report != null && CurrentTransporteur != null && report.HasAccess(CurrentTransporteur))
                {
                    var reportAnswer = (ReportAnswer)ModelMapper.Map(reportAnswerView, typeof(ReportAnswerView), typeof(ReportAnswer));
                    reportAnswer.TransporteurID = CurrentTransporteur.ID;
                    Repository.CreateReportAnswer(reportAnswer);

                    report.Status = (int)Report.StatusEnum.Answered;
                    Repository.UpdateReport(report);

                    return View("Thanks");
                }
                return null;
            }
            return View(reportAnswerView);
        }
    }
}