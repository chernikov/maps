using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using maps.Model;
using maps.Web.Mappers;
using maps.Web.Models.Api;
using maps.Web.Models.Dto;
using Ninject;

namespace maps.Web.Api.Controllers
{
    public class BycicleParkingController : BaseApiController
    {
        // GET api/values
        public IEnumerable<BicycleParkingDto> Get()
        {
            var source = Repository.BicycleParkings.Where(p => p.VerifiedDate.HasValue || !p.Exist);
            var list = (List<BicycleParkingDto>)ModelMapper.Map(source, typeof(IQueryable<BicycleParking>), typeof(List<BicycleParkingDto>));
            return list;
        }

        // POST api/values
        public object Post([FromBody]BicycleParkingApiModel value)
        {
            if (ModelState.IsValid)
            {
                var bicycleParking =
                    (BicycleParking) ModelMapper.Map(value, typeof (BicycleParkingApiModel), typeof (BicycleParking));
                bicycleParking.UserID = 3; //admin
                bicycleParking.CityID = 1;
                Repository.CreateBicycleParking(bicycleParking);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    result = "ok",
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                result = "error",
                value
            });
        }

        // PUT api/values/5
        public object Put(int id, [FromBody]BicycleParkingApiModel value)
        {
            if (ModelState.IsValid)
            {
                var bicycleParking =
                    (BicycleParking)ModelMapper.Map(value, typeof(BicycleParkingApiModel), typeof(BicycleParking));
                Repository.UpdateBicycleParking(bicycleParking);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    result = "ok",
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                result = "error",
                value
            });

        }

        // DELETE api/values/5
        public object Delete(int id)
        {
            Repository.RemoveBicycleParking(id);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                result = "ok",
            });
        }
    }
}