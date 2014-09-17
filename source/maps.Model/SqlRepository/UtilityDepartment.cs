using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UtilityDepartment> UtilityDepartments
        {
            get
            {
                return Db.UtilityDepartments;
            }
        }

        public bool CreateUtilityDepartment(UtilityDepartment instance)
        {
            if (instance.ID == 0)
            {
                Db.UtilityDepartments.InsertOnSubmit(instance);
                Db.UtilityDepartments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUtilityDepartment(UtilityDepartment instance)
        {
            var cache = Db.UtilityDepartments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Name = instance.Name;
				cache.Phone = instance.Phone;
				cache.Address = instance.Address;
                Db.UtilityDepartments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUtilityDepartment(int idUtilityDepartment)
        {
            UtilityDepartment instance = Db.UtilityDepartments.FirstOrDefault(p => p.ID == idUtilityDepartment);
            if (instance != null)
            {
                Db.UtilityDepartments.DeleteOnSubmit(instance);
                Db.UtilityDepartments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}