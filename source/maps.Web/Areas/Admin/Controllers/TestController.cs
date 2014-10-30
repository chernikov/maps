using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Mail;

namespace maps.Web.Areas.Admin.Controllers
{
    public class TestController : AdminController
    {
        public ActionResult Index()
        {
            var mailSender = new MailSender();

            mailSender.SendMail("chernikov@gmail.com", "Test", "Test");
            return Content("OK");
        }

        public ActionResult Upload()
        {
            return View();
        }
	}
}