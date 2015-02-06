using maps.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public class MapsUnitOfWorkFactory : IUnitOfWorkFactory<IMapsContext>
    {
        public IUnitOfWork<IMapsContext> Create()
        {
            return new UnitOfWork<MapsContext>();
        }
    }
}
