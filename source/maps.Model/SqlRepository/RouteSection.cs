using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<RouteSection> RouteSections
        {
            get
            {
                return Db.RouteSections;
            }
        }

        public bool CreateRouteSection(RouteSection instance)
        {
            if (instance.ID == 0)
            {
                Db.RouteSections.InsertOnSubmit(instance);
                Db.RouteSections.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRouteSection(int idRouteSection)
        {
            RouteSection instance = Db.RouteSections.Where(p => p.ID == idRouteSection).FirstOrDefault();
            if (instance != null)
            {
                Db.RouteSections.DeleteOnSubmit(instance);
                Db.RouteSections.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
