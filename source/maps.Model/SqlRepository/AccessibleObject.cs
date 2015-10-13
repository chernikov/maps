using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<AccessibleObject> AccessibleObjects
        {
            get
            {
                return Db.AccessibleObjects;
            }
        }

        public bool CreateAccessibleObject(AccessibleObject instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.AccessibleObjects.InsertOnSubmit(instance);
                Db.AccessibleObjects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateAccessibleObject(AccessibleObject instance)
        {
            var cache = Db.AccessibleObjects.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.UserID = instance.UserID;
                cache.CityID = instance.CityID;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.Type = instance.Type;
                cache.Address = instance.Address;
                cache.Description = instance.Description;
                Db.AccessibleObjects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool VerifiedAccessibleObject(int idAccessibleObject, bool verified)
        {
            var cache = Db.AccessibleObjects.FirstOrDefault(p => p.ID == idAccessibleObject);
            if (cache != null)
            {
                if (verified)
                {
                    cache.VerifiedDate = DateTime.Now;
                }
                else
                {
                    cache.VerifiedDate = null;
                }
                Db.AccessibleObjects.Context.SubmitChanges();
                return true;
            }
            return false;
        }



        public bool RemoveAccessibleObject(int idAccessibleObject)
        {
            var instance = Db.AccessibleObjects.Where(p => p.ID == idAccessibleObject).FirstOrDefault();
            if (instance != null)
            {
                Db.AccessibleObjects.DeleteOnSubmit(instance);
                Db.AccessibleObjects.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
