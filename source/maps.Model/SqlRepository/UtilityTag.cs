using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UtilityTag> UtilityTags
        {
            get
            {
                return Db.UtilityTags;
            }
        }

        public bool CreateUtilityTag(UtilityTag instance)
        {
            if (instance.ID == 0)
            {
                Db.UtilityTags.InsertOnSubmit(instance);
                Db.UtilityTags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUtilityTag(UtilityTag instance)
        {
            var cache = Db.UtilityTags.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Name = instance.Name;
                Db.UtilityTags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUtilityTag(int idUtilityTag)
        {
            UtilityTag instance = Db.UtilityTags.FirstOrDefault(p => p.ID == idUtilityTag);
            if (instance != null)
            {
                Db.UtilityTags.DeleteOnSubmit(instance);
                Db.UtilityTags.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}