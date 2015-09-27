using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<AccessibleDirection> AccessibleDirections
        {
            get
            {
                return Db.AccessibleDirections;
            }
        }

        public bool CreateAccessibleDirection(AccessibleDirection instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.AccessibleDirections.InsertOnSubmit(instance);
                Db.AccessibleDirections.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateAccessibleDirection(AccessibleDirection instance)
        {
            var cache = Db.AccessibleDirections.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.CityID = instance.CityID;
                cache.UserID = instance.UserID;
                cache.Waypoints = instance.Waypoints;
                cache.PolyLine = instance.PolyLine;
                cache.Length = instance.Length;
                Db.AccessibleDirections.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveAccessibleDirection(int idAccessibleDirection)
        {
            var instance = Db.AccessibleDirections.Where(p => p.ID == idAccessibleDirection).FirstOrDefault();
            if (instance != null)
            {
                Db.AccessibleDirections.DeleteOnSubmit(instance);
                Db.AccessibleDirections.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
