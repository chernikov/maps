using maps.Data;
using maps.Data.Entities;
using maps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace maps.Core
{
    public class BicycleParkingManager : BaseManager
    {
        public BicycleParkingManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IList<BicycleParking> BicycleParkings
        {
            get
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    return unitOfWork.Repository<BicycleParking>().Entities.ToList();
                }
            }
        }
    }
}
