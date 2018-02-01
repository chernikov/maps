using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using maps.Model;
using maps.Web.Mappers;
using Ninject;

namespace maps.Web.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        protected string DestinationDir = "Content/files/uploads/";

        public IRepository Repository { get; set; }

        public ModelMapper ModelMapper { get; set; }

        public BaseApiController()
        {
            Repository = DependencyResolver.Current.GetService<IRepository>();
            ModelMapper = DependencyResolver.Current.GetService<ModelMapper>();
        }
    }
}