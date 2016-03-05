using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<VisualizationItem> VisualizationItems
        {
            get
            {
                return Db.VisualizationItems;
            }
        }

        public bool CreateVisualizationItem(VisualizationItem instance)
        {
            if (instance.ID == 0)
            {
                Db.VisualizationItems.InsertOnSubmit(instance);
                Db.VisualizationItems.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateVisualizationItem(VisualizationItem instance)
        {
            var cache = Db.VisualizationItems.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.Data = instance.Data;
                Db.VisualizationItems.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveVisualizationItem(int idVisualizationItem)
        {
            var instance = Db.VisualizationItems.Where(p => p.ID == idVisualizationItem).FirstOrDefault();
            if (instance != null)
            {
                Db.VisualizationItems.DeleteOnSubmit(instance);
                Db.VisualizationItems.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
