using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Tool;
using maps.Web.Models.ViewModels.User;
using maps.UnitTest.Mock.Http;

namespace maps.UnitTest
{
    [TestFixture]
    public class DefaultUserControllerTest
    {
        [Test]
        public void Index_AskForDefaultPage_GetViewResult()
        {
            var controller = new maps.Web.Areas.Default.Controllers.UserController();

            var registerUserView = new RegisterUserView()
            {
                ID = 0,
                Email = "rollinx@gmail.com",
                Password = "123456",
                ConfirmPassword = "654321",
                Captcha = "1111"
            };
        }

        /*[Test]
        public void Register_CaptchaIsInCorrect_Fail()
        {
            var httpContext = new MockHttpContext().Object;
            var controller = DependencyResolver.Current.GetService<Web.Areas.Default.Controllers.UserController>();
            var route = new RouteData();
            route.Values.Add("controller", "User");
            route.Values.Add("action", "Register");
            route.Values.Add("area", "Default");

            ControllerContext context = new ControllerContext(new RequestContext(httpContext, route), controller);
            controller.ControllerContext = context;

            controller.Session.Add(CaptchaImage.CaptchaValueKey, "1112");
            var registerUserView = new RegisterUserView()
            {
                ID = 0,
                Captcha = "1111"
            };
            controller.Register(registerUserView);
        }*/
    }
}
