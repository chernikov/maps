namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityIssueTag")]
    public partial class UtilityIssueTag
    {
        public int ID { get; set; }

        public int UtilityIssueID { get; set; }

        public int UtilityTagID { get; set; }

        public virtual UtilityIssue UtilityIssue { get; set; }

        public virtual UtilityTag UtilityTag { get; set; }
    }
}
