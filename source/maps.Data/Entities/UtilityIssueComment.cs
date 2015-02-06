namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityIssueComment")]
    public partial class UtilityIssueComment
    {
        public int ID { get; set; }

        public int UtilityIssueID { get; set; }

        public int CommentID { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual UtilityIssue UtilityIssue { get; set; }
    }
}
