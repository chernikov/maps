using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UtilityIssueTag> UtilityIssueTags
        {
            get
            {
                return Db.UtilityIssueTags;
            }
        }

        public bool CreateUtilityIssueTag(UtilityIssueTag instance)
        {
            if (instance.ID == 0)
            {
                Db.UtilityIssueTags.InsertOnSubmit(instance);
                Db.UtilityIssueTags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUtilityIssueTag(UtilityIssueTag instance)
        {
            var cache = Db.UtilityIssueTags.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UtilityIssueID = instance.UtilityIssueID;
				cache.UtilityTagID = instance.UtilityTagID;
                Db.UtilityIssueTags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUtilityIssueTag(int idUtilityIssueTag)
        {
            UtilityIssueTag instance = Db.UtilityIssueTags.FirstOrDefault(p => p.ID == idUtilityIssueTag);
            if (instance != null)
            {
                Db.UtilityIssueTags.DeleteOnSubmit(instance);
                Db.UtilityIssueTags.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}