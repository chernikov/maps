using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<BusPhoto> BusPhotos
        {
            get
            {
                return Db.BusPhotos;
            }
        }

        public bool CreateBusPhoto(BusPhoto instance)
        {
            if (instance.ID == 0)
            {
                Db.BusPhotos.InsertOnSubmit(instance);
                Db.BusPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBusPhoto(int idBusPhoto)
        {
            BusPhoto instance = Db.BusPhotos.Where(p => p.ID == idBusPhoto).FirstOrDefault();
            if (instance != null)
            {
                Db.BusPhotos.DeleteOnSubmit(instance);
                Db.BusPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
