using maps.Web.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Web.Api
{
    public class HousingController : BaseApiController
    {
        public class RequestFilter
        {
            public int StartYear { get; set; }

            public int LastYear { get; set; }

            public bool ExistOnStartYear { get; set; }
        }

        public class BuildingDto
        {
            public int Id { get; set; }

            public string Address { get; set; }

            public int Capacity { get; set; }

            public int? OnStart { get; set; }

            public int? OnEnd { get; set; }

            public double Lat { get; set; }

            public double Lng { get; set; }
        }

        public IEnumerable<BuildingDto> Post(RequestFilter request)
        {
            var result = new List<BuildingDto>();
            if (request == null)
            {
                request = new RequestFilter()
                {
                    StartYear = 2005,
                    LastYear = 2015
                };
            }
            if (request.StartYear == 0)
            {
                request.StartYear = 2005;
            }
            if (request.LastYear == 0)
            {
                request.LastYear = 2015;
            }

            var list = Repository.Buildings.ToList();

            foreach(var item in list)
            {
                var start = item.BuildingElectricities.FirstOrDefault(p => p.Year == request.StartYear);
                var end = item.BuildingElectricities.FirstOrDefault(p => p.Year == request.LastYear);
                
                if ((start != null || !request.ExistOnStartYear) && end != null)
                {
                    var entity = new BuildingDto()
                    {
                        Id = item.ID,
                        Address = item.Address,
                        Lat = item.Lat,
                        Lng = item.Lng,
                        Capacity = item.Capacity ?? 0,
                        OnStart = start?.PowerOn,
                        OnEnd = end?.PowerOn
                    };
                    result.Add(entity);
                }
            }

            return result;
        }
    }
}
