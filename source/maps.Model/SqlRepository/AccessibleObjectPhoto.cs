using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<AccessibleObjectPhoto> AccessibleObjectPhotos
        {
            get
            {
                return Db.AccessibleObjectPhotos;
            }
        }

        public bool CreateAccessibleObjectPhoto(AccessibleObjectPhoto instance)
        {
            if (instance.ID == 0)
            {
                Db.AccessibleObjectPhotos.InsertOnSubmit(instance);
                Db.AccessibleObjectPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateAccessibleObjectPhoto(AccessibleObjectPhoto instance)
        {
            var cache = Db.AccessibleObjectPhotos.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.AccessibleObjectID = instance.AccessibleObjectID;
                Db.UtilityPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveAccessibleObjectPhoto(int idAccessibleObjectPhoto)
        {
            var instance = Db.AccessibleObjectPhotos.Where(p => p.ID == idAccessibleObjectPhoto).FirstOrDefault();
            if (instance != null)
            {
                Db.AccessibleObjectPhotos.DeleteOnSubmit(instance);
                Db.AccessibleObjectPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
