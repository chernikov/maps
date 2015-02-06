namespace maps.Data.Entities
{
    using maps.Data.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityIssueHistory")]
    public partial class UtilityIssueHistory : DatabaseEntity
    {
        public UtilityIssueHistory()
        {
            UtilityIssueHistorys = new HashSet<UtilityIssueHistory>();
        }

        public int ID { get; set; }

        public int UtilityIssueID { get; set; }

        public int UserID { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? AcceptedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int? UtilityDepartmentID { get; set; }

        public int? MainUtilityIssueID { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Solution { get; set; }

        public int Status { get; set; }

        public bool IsRemoved { get; set; }

        public virtual User User { get; set; }

        public virtual UtilityDepartment UtilityDepartment { get; set; }

        public virtual UtilityIssue UtilityIssue { get; set; }

        public virtual ICollection<UtilityIssueHistory> UtilityIssueHistorys { get; set; }

        public virtual UtilityIssueHistory MainUtilityIssueHistory { get; set; }
    }
}
