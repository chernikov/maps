using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<Visualization> Visualizations
        {
            get
            {
                return Db.Visualizations;
            }
        }

        public bool CreateVisualization(Visualization instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.Visualizations.InsertOnSubmit(instance);
                Db.Visualizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateVisualization(Visualization instance)
        {
            var cache = Db.Visualizations.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                Db.Visualizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveVisualization(int idVisualization)
        {
            var instance = Db.Visualizations.Where(p => p.ID == idVisualization).FirstOrDefault();
            if (instance != null)
            {
                Db.Visualizations.DeleteOnSubmit(instance);
                Db.Visualizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
