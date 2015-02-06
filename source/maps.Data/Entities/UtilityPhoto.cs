namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityPhoto")]
    public partial class UtilityPhoto
    {
        public int ID { get; set; }

        public int? UtilityIssueID { get; set; }

        public int? UserID { get; set; }

        [Required]
        [StringLength(150)]
        public string Image { get; set; }

        public DateTime AddedDate { get; set; }

        public int State { get; set; }

        public bool IsRemoved { get; set; }

        public virtual User User { get; set; }

        public virtual UtilityIssue UtilityIssue { get; set; }
    }
}
