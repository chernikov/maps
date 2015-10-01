using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<AccessiblePlacePhoto> AccessiblePlacePhotos
        {
            get
            {
                return Db.AccessiblePlacePhotos;
            }
        }

        public bool CreateAccessiblePlacePhoto(AccessiblePlacePhoto instance)
        {
            if (instance.ID == 0)
            {
                Db.AccessiblePlacePhotos.InsertOnSubmit(instance);
                Db.AccessiblePlacePhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateAccessiblePlacePhoto(AccessiblePlacePhoto instance)
        {
            var cache = Db.AccessiblePlacePhotos.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.AccessiblePlaceID = instance.AccessiblePlaceID;
                Db.UtilityPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveAccessiblePlacePhoto(int idAccessiblePlacePhoto)
        {
            var instance = Db.AccessiblePlacePhotos.Where(p => p.ID == idAccessiblePlacePhoto).FirstOrDefault();
            if (instance != null)
            {
                Db.AccessiblePlacePhotos.DeleteOnSubmit(instance);
                Db.AccessiblePlacePhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
