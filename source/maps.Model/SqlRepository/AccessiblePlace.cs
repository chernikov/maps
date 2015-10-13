using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<AccessiblePlace> AccessiblePlaces
        {
            get
            {
                return Db.AccessiblePlaces;
            }
        }

        public bool CreateAccessiblePlace(AccessiblePlace instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.AccessiblePlaces.InsertOnSubmit(instance);
                Db.AccessiblePlaces.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateAccessiblePlace(AccessiblePlace instance)
        {
            var cache = Db.AccessiblePlaces.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.UserID = instance.UserID;
                cache.CityID = instance.CityID;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.Address = instance.Address;
                cache.Description = instance.Description;
                Db.AccessiblePlaces.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool VerifiedAccessiblePlace(int idAccessiblePlace, bool verified)
        {
            var cache = Db.AccessiblePlaces.FirstOrDefault(p => p.ID == idAccessiblePlace);
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
                Db.AccessiblePlaces.Context.SubmitChanges();
                return true;
            }
            return false;
        }



        public bool RemoveAccessiblePlace(int idAccessiblePlace)
        {
            var instance = Db.AccessiblePlaces.Where(p => p.ID == idAccessiblePlace).FirstOrDefault();
            if (instance != null)
            {
                Db.AccessiblePlaces.DeleteOnSubmit(instance);
                Db.AccessiblePlaces.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
