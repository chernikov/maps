using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UtilityIssueComment> UtilityIssueComments
        {
            get
            {
                return Db.UtilityIssueComments;
            }
        }

        public bool CreateUtilityIssueComment(UtilityIssueComment instance)
        {
            if (instance.ID == 0)
            {
                Db.UtilityIssueComments.InsertOnSubmit(instance);
                Db.UtilityIssueComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUtilityIssueComment(UtilityIssueComment instance)
        {
            var cache = Db.UtilityIssueComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UtilityIssueID = instance.UtilityIssueID;
				cache.CommentID = instance.CommentID;
                Db.UtilityIssueComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUtilityIssueComment(int idUtilityIssueComment)
        {
            UtilityIssueComment instance = Db.UtilityIssueComments.FirstOrDefault(p => p.ID == idUtilityIssueComment);
            if (instance != null)
            {
                Db.UtilityIssueComments.DeleteOnSubmit(instance);
                Db.UtilityIssueComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}