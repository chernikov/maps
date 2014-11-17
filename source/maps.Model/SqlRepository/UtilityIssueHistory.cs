using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{

    public partial class SqlRepository
    {
        public IQueryable<UtilityIssueHistory> UtilityIssueHistories
        {
            get
            {
                return Db.UtilityIssueHistories;
            }
        }

        private void MakeUtilityIssueHistory(UtilityIssue instance, int userId)
        {
            Db.UtilityIssueHistories.InsertOnSubmit(new UtilityIssueHistory()
            {
                UtilityIssueID = instance.ID,
                UserID = userId,
                HistoryDate = DateTime.Now,
                AddedDate = instance.AddedDate,
                AcceptedDate = instance.AcceptedDate,
                ResolvedDate = instance.ResolvedDate,
                ClosedDate = instance.ClosedDate,
                UtilityDepartmentID = instance.UtilityDepartmentID,
                MainUtilityIssueID = instance.MainUtilityIssueID,
                Lat = instance.Lat,
                Lng = instance.Lng,
                Address = instance.Address,
                Description = instance.Description,
                WorkFlow = instance.WorkFlow,
                Solution = instance.Solution,
                Status = instance.Status,
                IsRemoved = instance.IsRemoved,
            });
            Db.UtilityIssueHistories.Context.SubmitChanges();
        }
    }
}