namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public Comment()
        {
            Comments = new HashSet<Comment>();
            UtilityIssueComments = new HashSet<UtilityIssueComment>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int? ParentID { get; set; }

        public DateTime AddedDate { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<UtilityIssueComment> UtilityIssueComments { get; set; }
    }
}
