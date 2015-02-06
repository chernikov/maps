using maps.Data;
using maps.Data.Entities;
using maps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity;

namespace maps.Core
{
    public class CityManager : BaseManager
    {
        public CityManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IList<City> Cities
        {
            get
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    return unitOfWork.Repository<City>().Entities.ToList();
                }
            }
        }
    }
}
