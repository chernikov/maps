using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using maps.Model;
using maps.Web.Mappers;
using Ninject;

namespace maps.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        public IRepository Repository { get; set; }

        public ModelMapper ModelMapper { get; set; }

        public BaseApiController()
        {
            Repository = DependencyResolver.Current.GetService<IRepository>();
            ModelMapper = DependencyResolver.Current.GetService<ModelMapper>();
        }
    }
}