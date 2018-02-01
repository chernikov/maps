using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<BuildingElectricity> BuildingElectricities
        {
            get
            {
                return Db.BuildingElectricities;
            }
        }

        public bool CreateBuildingElectricity(BuildingElectricity instance)
        {
            if (instance.ID == 0)
            {
                Db.BuildingElectricities.InsertOnSubmit(instance);
                Db.BuildingElectricities.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBuildingElectricity(BuildingElectricity instance)
        {
            var cache = Db.BuildingElectricities.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Year = instance.Year;
                cache.PowerOn = instance.PowerOn;
                cache.Consumed = instance.Consumed;
                Db.BuildingElectricities.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBuildingElectricity(int idBuildingElectricity)
        {
            var instance = Db.BuildingElectricities.FirstOrDefault(p => p.ID == idBuildingElectricity);
            if (instance != null)
            {
                Db.BuildingElectricities.DeleteOnSubmit(instance);
                Db.BuildingElectricities.Context.SubmitChanges();
                return true;
            }

            return false;
        }

    }
}
