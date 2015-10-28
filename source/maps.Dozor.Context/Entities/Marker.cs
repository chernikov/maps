using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Context.Entities
{
    public class Marker
    {
        public int Id { get; set; }

        public int BusId { get; set; }

        public Bus Bus { get; set; }

        public int Lng { get; set; }

        public int Lat { get; set; }

        public int Speed { get; set; }

        public int TrackerTime { get; set; }

        public int ServerTime { get; set; }

        public string Place { get; set;  }
    }
}
