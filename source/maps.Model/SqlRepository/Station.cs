using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Station> Stations
        {
            get
            {
                return Db.Stations;
            }
        }

        public bool CreateStation(Station instance)
        {
            if (instance.ID == 0)
            {
                Db.Stations.InsertOnSubmit(instance);
                Db.Stations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStation(Station instance)
        {
            Station cache = Db.Stations.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.IsEndStation = instance.IsEndStation;
                cache.HasPocket = instance.HasPocket;
                cache.HasNewTimetable = instance.HasNewTimetable;
                Db.Stations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStation(int idStation)
        {
            Station instance = Db.Stations.Where(p => p.ID == idStation).FirstOrDefault();
            if (instance != null)
            {
                Db.Stations.DeleteOnSubmit(instance);
                Db.Stations.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
