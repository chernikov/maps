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
    public class BicycleDirectionManager : BaseManager
    {
        public BicycleDirectionManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public IList<BicycleDirectionLine> BicycleDirectionLines
        {
            get
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    return unitOfWork.Repository<BicycleDirectionLine>().Entities.ToList();
                }
            }
        }
    }
}
