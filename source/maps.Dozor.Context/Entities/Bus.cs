using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Context.Entities
{
    public class Bus
    {
        public int Id { get; set; }

        public string Name { get; set;  }

        public int GpsId { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
