using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Brand> Brands
        {
            get
            {
                return Db.Brands;
            }
        }

        public bool CreateBrand(Brand instance)
        {
            if (instance.ID == 0)
            {
                Db.Brands.InsertOnSubmit(instance);
                Db.Brands.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBrand(Brand instance)
        {
            Brand cache = Db.Brands.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                Db.Brands.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBrand(int idBrand)
        {
            Brand instance = Db.Brands.Where(p => p.ID == idBrand).FirstOrDefault();
            if (instance != null)
            {
                Db.Brands.DeleteOnSubmit(instance);
                Db.Brands.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
