using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Social> Socials
        {
            get
            {
                return Db.Socials;
            }
        }

        public bool CreateSocial(Social instance)
        {
            if (instance.ID == 0)
            {
                Db.Socials.InsertOnSubmit(instance);
                Db.Socials.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSocial(Social instance)
        {
            var cache = Db.Socials.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.Identified = instance.Identified;
				cache.Provider = instance.Provider;
				cache.UserInfo = instance.UserInfo;
				cache.JsonResource = instance.JsonResource;
                Db.Socials.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSocial(int idSocial)
        {
            Social instance = Db.Socials.FirstOrDefault(p => p.ID == idSocial);
            if (instance != null)
            {
                Db.Socials.DeleteOnSubmit(instance);
                Db.Socials.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}