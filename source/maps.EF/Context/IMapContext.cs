using maps.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.EF.Context
{
    public interface IMapContext
    {
        public IDbSet<BicycleLine> BicycleLine { get; set; }

        public IDbSet<BicycleLine> BicycleLine { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Role> Roles { get; set; }
    }
}
