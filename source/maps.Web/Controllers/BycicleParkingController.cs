using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using maps.Model;
using maps.Web.Mappers;
using maps.Web.Models.Api;
using maps.Web.Models.Dto;
using Ninject;

namespace maps.Web.Controllers
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
        public void Post([FromBody]BicycleParkingApiModel value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]BicycleParkingDto value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}