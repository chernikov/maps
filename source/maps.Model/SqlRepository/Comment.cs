using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Comment> Comments
        {
            get
            {
                return Db.Comments;
            }
        }

        public bool CreateComment(Comment instance)
        {
            if (instance.ID == 0)
            {
                Db.Comments.InsertOnSubmit(instance);
                instance.AddedDate = DateTime.Now;
                Db.Comments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateComment(Comment instance)
        {
            var cache = Db.Comments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.ParentID = instance.ParentID;
				cache.AddedDate = instance.AddedDate;
				cache.Text = instance.Text;
                Db.Comments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveComment(int idComment)
        {
            Comment instance = Db.Comments.FirstOrDefault(p => p.ID == idComment);
            if (instance != null)
            {
                Db.Comments.DeleteOnSubmit(instance);
                Db.Comments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}