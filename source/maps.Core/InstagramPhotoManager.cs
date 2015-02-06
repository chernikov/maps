using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using maps.Repository;
using maps.Data;

namespace maps.Core
{
    public class InstagramPhotoManager : BaseManager
    {
        public InstagramPhotoManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }
    }
}
