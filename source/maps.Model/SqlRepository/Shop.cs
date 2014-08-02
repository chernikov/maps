using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Shop> Shops
        {
            get
            {
                return Db.Shops;
            }
        }

        public bool CreateShop(Shop instance)
        {
            if (instance.Id == 0)
            {
                Db.Shops.InsertOnSubmit(instance);
                Db.Shops.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateShop(Shop instance)
        {
            var cache = Db.Shops.FirstOrDefault(p => p.Id == instance.Id);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.Position = instance.Position;
                Db.Shops.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveShop(int idShop)
        {
            Shop instance = Db.Shops.FirstOrDefault(p => p.Id == idShop);
            if (instance != null)
            {
                Db.Shops.DeleteOnSubmit(instance);
                Db.Shops.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}