namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityDepartment")]
    public partial class UtilityDepartment
    {
        public UtilityDepartment()
        {
            UtilityIssues = new HashSet<UtilityIssue>();
            UtilityIssueHistories = new HashSet<UtilityIssueHistory>();
        }

        public int ID { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<UtilityIssue> UtilityIssues { get; set; }

        public virtual ICollection<UtilityIssueHistory> UtilityIssueHistories { get; set; }
    }
}
