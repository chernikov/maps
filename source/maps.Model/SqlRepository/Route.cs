using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Route> Routes
        {
            get
            {
                return Db.Routes;
            }
        }

        public bool CreateRoute(Route instance)
        {
            if (instance.ID == 0)
            {
                Db.Routes.InsertOnSubmit(instance);
                Db.Routes.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateRoute(Route instance)
        {
            Route cache = Db.Routes.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                cache.Length = instance.Length;
                Db.Routes.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRoute(int idRoute)
        {
            Route instance = Db.Routes.Where(p => p.ID == idRoute).FirstOrDefault();
            if (instance != null)
            {
                Db.Routes.DeleteOnSubmit(instance);
                Db.Routes.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
