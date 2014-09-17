using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
     public IQueryable<UtilityPhoto> UtilityPhotos
        {
            get
            {
                return Db.UtilityPhotos;
            }
        }

        public bool CreateUtilityPhoto(UtilityPhoto instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.UtilityPhotos.InsertOnSubmit(instance);
                Db.UtilityPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUtilityPhoto(UtilityPhoto instance)
        {
            var cache = Db.UtilityPhotos.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UtilityIssueID = instance.UtilityIssueID;
				cache.UserID = instance.UserID;
                cache.IsRemoved = instance.IsRemoved;
				cache.State = instance.State;
                Db.UtilityPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveUtilityPhoto(int idUtilityPhoto)
        {
            UtilityPhoto instance = Db.UtilityPhotos.FirstOrDefault(p => p.ID == idUtilityPhoto);
            if (instance != null)
            {
                instance.IsRemoved = true;
                Db.UtilityPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}