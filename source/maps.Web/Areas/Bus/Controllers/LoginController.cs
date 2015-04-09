using maps.Model;
using maps.Web.Models.ViewModels;
using maps.Web.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;

namespace maps.Web.Areas.Bus.Controllers
{
    public class LoginController : BaseBusController
    {
        [HttpGet]
        public ActionResult Index(int id)
        {
            var report = Repository.Reports.FirstOrDefault(p => p.ID == id);
            if (report != null && report.Bus != null)
            {
                var transporteur = report.Bus.Transporteur;
                var transporteurLoginView = new TransporteurLoginView()
                {
                    ID = id,
                    _ID = transporteur.ID,
                    Mobile = transporteur.PrimaryPhone,
                    UserIsExist = transporteur.User != null,
                };
                if (!string.IsNullOrWhiteSpace(transporteur.AdditionalPhone))
                {
                    transporteurLoginView.Mobiles = new List<string> {
                        transporteur.PrimaryPhone, 
                        transporteur.AdditionalPhone
                    };
                }
                return View(transporteurLoginView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Index(TransporteurLoginView transporteurLoginView)
        {
            if (ModelState.IsValid)
            {
                var transporteur = Repository.Transporteurs.FirstOrDefault(p => p.ID == transporteurLoginView._ID);
                if (transporteur != null)
                {
                    var code = Session["Code"] as string;
                    if (transporteur.UserID == null)
                    {
                         if (code == transporteurLoginView.Password)
                         {
                             var user = new User()
                             {
                                 Login = Translit.Translate(string.Format("{0}_{1}_{2}", transporteur.LastName, transporteur.FirstName, transporteur.Patronymic)),
                                 FirstName = transporteur.FirstName,
                                 LastName = transporteur.LastName,
                                 Mobile = "38" + transporteur.PrimaryPhone.ClearPhone(),
                                 Password = transporteurLoginView.Password, 
                                 CityID = CurrentCity.ID
                             };
                             Repository.CreateUser(user);
                             transporteur.UserID = user.ID;
                             Repository.UpdateTransporteur(transporteur);

                             var userRole = new UserRole()
                             {
                                 RoleID = Repository.Roles.First(p => p.Code == "transporteur").ID,
                                 UserID = user.ID
                             };
                             Repository.CreateUserRole(userRole);
                             Auth.Login(user.Login);
                             return RedirectToAction("Index", "Home");
                         }
                    }
                    else
                    {
                        if (transporteur.User.Password == transporteurLoginView.Password)
                        {
                            Auth.Login(transporteur.User.Login);
                            return RedirectToAction("Answer", "Report", new { id = transporteurLoginView.ID });
                        }
                    }
                    ModelState.AddModelError("Password", "Код не вірний");
                    return View(transporteurLoginView);
                }
                return RedirectToNotFoundPage;
            }
            return View(transporteurLoginView);
        }

        public ActionResult SendCode(string mobile)
        {
            var transporteur = Repository.Transporteurs.FirstOrDefault(p => p.PrimaryPhone == mobile || p.AdditionalPhone == mobile);
            if (transporteur != null) 
            {
                if (transporteur.User != null) 
                {
                    var result = SmsSender.SendSms(transporteur.User.Mobile, "Vash parol: " + transporteur.User.Password);

                     Repository.CreateNotify(new Notify()
                {
                   
                    Phone = transporteur.User.Mobile,
                    Result = result,
                    Text = "Vash parol: " + transporteur.User.Password,
                    Sender = "IFBus"
                });
                } 
                else 
                {
                    var code = StringExtension.CreateRandomPassword(6, "0123456789");
                    Session["Code"] = code;
                    var result = SmsSender.SendSms("38" + mobile.ClearPhone(), "Vash cod: " + code);

                    Repository.CreateNotify(new Notify()
                    {
                        ReportID = null,
                        Phone = "38" + mobile.ClearPhone(),
                        Result = result,
                        Text = "Vash parol': " + "Vash cod: " + code,
                        Sender = "IFBus"
                    });
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}