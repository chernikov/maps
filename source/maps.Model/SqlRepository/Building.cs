using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<Building> Buildings
        {
            get
            {
                return Db.Buildings;
            }
        }

        public bool CreateBuilding(Building instance)
        {
            if (instance.ID == 0)
            {
                Db.Buildings.InsertOnSubmit(instance);
                Db.Buildings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBuilding(Building instance)
        {
            var cache = Db.Buildings.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Address = instance.Address;
                cache.Capacity = instance.Capacity;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                Db.Buildings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBuilding(int idBuilding)
        {
            var instance = Db.Buildings.Where(p => p.ID == idBuilding).FirstOrDefault();
            if (instance != null)
            {
                Db.Buildings.DeleteOnSubmit(instance);
                Db.Buildings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

    }
}
