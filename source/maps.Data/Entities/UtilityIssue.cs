namespace maps.Data.Entities
{
    using maps.Data.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityIssue")]
    public partial class UtilityIssue : DatabaseEntity
    {
        public UtilityIssue()
        {
            UtilityIssues = new HashSet<UtilityIssue>();
            UtilityIssueComments = new HashSet<UtilityIssueComment>();
            UtilityIssueHistories = new HashSet<UtilityIssueHistory>();
            UtilityIssueTags = new HashSet<UtilityIssueTag>();
            UtilityPhotoes = new HashSet<UtilityPhoto>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int CityID { get; set; }

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

        public virtual City City { get; set; }

        public virtual User User { get; set; }

        public virtual UtilityDepartment UtilityDepartment { get; set; }

        public virtual ICollection<UtilityIssue> UtilityIssues { get; set; }

        public virtual UtilityIssue MainUtilityIssue { get; set; }

        public virtual ICollection<UtilityIssueComment> UtilityIssueComments { get; set; }

        public virtual ICollection<UtilityIssueHistory> UtilityIssueHistories { get; set; }

        public virtual ICollection<UtilityIssueTag> UtilityIssueTags { get; set; }

        public virtual ICollection<UtilityPhoto> UtilityPhotoes { get; set; }
    }
}
