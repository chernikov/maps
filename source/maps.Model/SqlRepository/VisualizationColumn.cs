using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<VisualizationColumn> VisualizationColumns
        {
            get
            {
                return Db.VisualizationColumns;
            }
        }

        public bool CreateVisualizationColumn(VisualizationColumn instance)
        {
            if (instance.ID == 0)
            {
                Db.VisualizationColumns.InsertOnSubmit(instance);
                Db.VisualizationColumns.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateVisualizationColumn(VisualizationColumn instance)
        {
            var cache = Db.VisualizationColumns.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                cache.Number = instance.Number;
                cache.Type = instance.Type;
                cache.FilterValues = instance.FilterValues;
                Db.VisualizationColumns.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveVisualizationColumn(int idVisualizationColumn)
        {
            var instance = Db.VisualizationColumns.Where(p => p.ID == idVisualizationColumn).FirstOrDefault();
            if (instance != null)
            {
                Db.VisualizationColumns.DeleteOnSubmit(instance);
                Db.VisualizationColumns.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
