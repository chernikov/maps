using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{

    public partial class SqlRepository
    {
        public IQueryable<UtilityIssue> UtilityIssues
        {
            get
            {
                return Db.UtilityIssues.Where(p => !p.IsRemoved);
            }
        }

        public bool CreateUtilityIssue(UtilityIssue instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.Status = (int)UtilityIssue.StatusType.Create;
                Db.UtilityIssues.InsertOnSubmit(instance);
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(instance, instance.UserID);
                return true;
            }

            return false;
        }

        public bool UpdateUtilityIssue(UtilityIssue instance, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.UtilityDepartmentID = instance.UtilityDepartmentID;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.Address = instance.Address;
                cache.Description = instance.Description;
                cache.WorkFlow = instance.WorkFlow;
                cache.Solution = instance.Solution;
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);
                return true;
            }

            return false;
        }


        public bool AcceptUtilityIssue(UtilityIssue instance, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                if (cache.Status == (int)UtilityIssue.StatusType.Create)
                {
                    cache.Status = (int)UtilityIssue.StatusType.Accept;
                    cache.AcceptedDate = DateTime.Now;
                }
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);
               
                return true;
            }

            return false;
        }

        public bool AcceptBackUtilityIssue(UtilityIssue instance, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {

                cache.Status = (int)UtilityIssue.StatusType.Accept;
                cache.ResolvedDate = null;
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);

                return true;
            }

            return false;
        }


        public bool ResolveUtilityIssue(UtilityIssue instance, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                if (cache.Status == (int)UtilityIssue.StatusType.Accept)
                {
                    cache.Status = (int)UtilityIssue.StatusType.Resolved;
                    cache.ResolvedDate = DateTime.Now;
                }
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);

                return true;
            }

            return false;
        }

        public bool CloseUtilityIssue(UtilityIssue instance, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                if (cache.Status == (int)UtilityIssue.StatusType.Create ||
                    cache.Status == (int)UtilityIssue.StatusType.Resolved ||
                    cache.Status == (int)UtilityIssue.StatusType.Resolved)
                {
                    cache.Status = (int)UtilityIssue.StatusType.Closed;
                    cache.ClosedDate = DateTime.Now;
                }
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);

                return true;
            }

            return false;
        }


        public bool RemoveUtilityIssue(int idUtilityIssue, int userID)
        {
            var cache = Db.UtilityIssues.FirstOrDefault(p => p.ID == idUtilityIssue);
            if (cache != null)
            {
                cache.IsRemoved = true;
                Db.UtilityIssues.Context.SubmitChanges();
                MakeUtilityIssueHistory(cache, userID);
                return true;
            }
            return false;
        }

        public bool ClearAllUtilityIssueTags(int idUtilityIssue)
        {
            var list = Db.UtilityIssueTags.Where(p => p.UtilityIssueID == idUtilityIssue).ToList();
            Db.UtilityIssueTags.DeleteAllOnSubmit(list);
            Db.UtilityIssueTags.Context.SubmitChanges();
            return true;
        }

        public bool ClearAllUtilityIssuePhotos(int idUtilityIssue)
        {
            var list = Db.UtilityPhotos.Where(p => p.UtilityIssueID == idUtilityIssue).ToList();
            foreach (var item in list)
            {
                item.IsRemoved = true;
            }
            Db.UtilityIssueTags.Context.SubmitChanges();
            return true;
        }
    }
}