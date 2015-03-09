using ImageResizer;
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
        [Authorize]
        public ActionResult Create()
        {
            return View(new NewReportView()
            {
                UserID = CurrentUser.ID,
                DateTime = DateTime.Now.Date.AddHours(12)
            });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(NewReportView newReportView)
        {
            if (ModelState.IsValid)
            {
                var report = (Report)ModelMapper.Map(newReportView, typeof(NewReportView), typeof(Report));
                report.UserID = CurrentUser.ID;
                var rules = Repository.Rules.Where(p => newReportView.SelectedRules.Contains(p.ID));
                report.Type = rules.Any(p => p.IsRouteScope) ? (int)Report.TypeEnum.RouteScope : (int)Report.TypeEnum.BusScope;
                report.Status = (int)Report.StatusEnum.New;
                Repository.CreateReport(report);

                if (newReportView.Photos != null)
                {
                    foreach (var reportPhotoView in newReportView.Photos)
                    {
                        var reportPhoto = Repository.ReportPhotos.FirstOrDefault(p => p.ID == reportPhotoView.Value.ID);
                        if (reportPhoto != null)
                        {
                            reportPhoto.ReportID = report.ID;
                            Repository.UpdateReportPhoto(reportPhoto);
                        }
                    }
                }
                if (newReportView.SelectedRules != null)
                {
                    foreach (var ruleId in newReportView.SelectedRules)
                    {
                        Repository.CreateRuleReport(new RuleReport()
                        {
                            ReportID = report.ID,
                            RuleID = ruleId
                        });
                    }
                }
            }
            return RedirectToAction("Index", "Home");
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
                var transporteur = report.Bus.Transporteur;
                var result = SmsSender.SendSms("38" + transporteur.PrimaryPhone.ClearPhone(),
                    string.Format("Porushennja! Bus " + report.Bus.Number + ". Bil'she dostupno po http://maps.if.ua/report/" + report.ID));
            }

            return RedirectToAction("Item", new { id });
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
                    return View("Thanks");
                }
                return null;
            }
            return View(reportAnswerView);
        }
    }
}