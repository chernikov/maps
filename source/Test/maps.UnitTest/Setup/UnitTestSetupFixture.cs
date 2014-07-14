using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using maps.Web.Global.Auth;
using maps.Web.Global.Config;
using maps.Model;
using maps.UnitTest.Fake;
using maps.UnitTest.Mock.Repository;
using maps.UnitTest.Tools;

namespace maps.UnitTest
{
    [SetUpFixture]
    public class UnitTestSetupFixture
    {
        protected static string Sandbox = "../../Sandbox";

        [SetUp]
        public virtual void Setup()
        {
            Console.WriteLine("===============");
            Console.WriteLine("Here we are go!");
            Console.WriteLine("===============");
            InitKernel();
            Console.WriteLine("===============");
            Console.WriteLine("Context Inited=");
            Console.WriteLine("===============");
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.WriteLine("===============");
            Console.WriteLine("=====BYE!======");
            Console.WriteLine("===============");
        }

        protected virtual IKernel InitKernel()
        {
            var kernel = new StandardKernel();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //IConfig
            InitConfig(kernel);
            //IRepository
            InitRepository(kernel);
            //Authentication
            InitAuth(kernel);
            return kernel;
        }

        protected virtual void InitAuth(StandardKernel kernel)
        {
            kernel.Bind<HttpCookieCollection>().To<HttpCookieCollection>();
            kernel.Bind<IAuthCookieProvider>().To<FakeAuthCookieProvider>().InSingletonScope();
            kernel.Bind<IAuthentication>().ToMethod<CustomAuthentication>(c =>
            {
                var auth = new CustomAuthentication();
                auth.AuthCookieProvider = kernel.Get<IAuthCookieProvider>();
                return auth;
            });
        }

        protected virtual void InitRepository(StandardKernel kernel)
        {
            kernel.Bind<MockRepository>().To<MockRepository>().InThreadScope();
            kernel.Bind<IRepository>().ToMethod(p => kernel.Get<MockRepository>().Object);
        }

        protected virtual void InitConfig(StandardKernel kernel)
        {
            Console.WriteLine("===============");
            Console.WriteLine("==Init Config==");
            Console.WriteLine("===============");
            var fullPath = new FileInfo(Sandbox + "/Web.config").FullName;
            kernel.Bind<IConfig>().ToMethod(c => new TestConfig(fullPath));
        }
    }
}
