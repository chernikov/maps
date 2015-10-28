using maps.Dozor.Context.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Context
{
    public class BusDbContext : DbContext 
    {
        public IDbSet<Bus> Buses { get; set; }

        public IDbSet<Marker> Marker { get; set; }


        public BusDbContext()
            : base("BusDbContext")
        {

        }
    }
}
