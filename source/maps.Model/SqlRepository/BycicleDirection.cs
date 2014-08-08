using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BycicleDirection> BycicleDirections
        {
            get
            {
                return Db.BycicleDirections;
            }
        }

        public bool CreateBycicleDirection(BycicleDirection instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.BycicleDirections.InsertOnSubmit(instance);
                Db.BycicleDirections.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ProcessBycicleDirection(int idBycicleDirection)
        {
            var instance = Db.BycicleDirections.FirstOrDefault(p => p.ID == idBycicleDirection && !p.Processed);
            if (instance != null)
            {
                instance.Processed = true;
                Db.BycicleDirections.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateBycicleDirection(BycicleDirection instance)
        {
            var cache = Db.BycicleDirections.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Waypoints = instance.Waypoints;
				cache.PolyLine = instance.PolyLine;
                cache.Length = instance.Length;
                Db.BycicleDirections.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBycicleDirection(int idBycicleDirection)
        {
            var instance = Db.BycicleDirections.FirstOrDefault(p => p.ID == idBycicleDirection);
            if (instance != null)
            {
                Db.BycicleDirections.DeleteOnSubmit(instance);
                Db.BycicleDirections.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}