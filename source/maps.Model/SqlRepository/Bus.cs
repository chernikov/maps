using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {


        public IQueryable<Bus> Bus
        {
            get
            {
                return Db.Bus;
            }
        }

        public bool CreateBus(Bus instance)
        {
            if (instance.ID == 0)
            {
                Db.Bus.InsertOnSubmit(instance);
                Db.Bus.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBus(Bus instance)
        {
            Bus cache = Db.Bus.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.BrandID = instance.BrandID;
                cache.Number = instance.Number;
                cache.TransporteurID = instance.TransporteurID;
                cache.RouteID = instance.RouteID;

                Db.Bus.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBus(int idBus)
        {
            Bus instance = Db.Bus.Where(p => p.ID == idBus).FirstOrDefault();
            if (instance != null)
            {
                Db.Bus.DeleteOnSubmit(instance);
                Db.Bus.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
