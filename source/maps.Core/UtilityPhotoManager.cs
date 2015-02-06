using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using maps.Repository;
using maps.Data;
using maps.Data.Entities;

namespace maps.Core
{
    public class UtilityPhotoManager : BaseManager
    {
        public UtilityPhotoManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }
        public IList<UtilityPhoto> UtilityPhotos
        {
            get
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    return unitOfWork.Repository<UtilityPhoto>().Entities.ToList();
                }
            }
        }
    }
}
