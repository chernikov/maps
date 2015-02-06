namespace maps.Data.Entities
{
    using maps.Data.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilityTag")]
    public partial class UtilityTag : DatabaseEntity
    {
        public UtilityTag()
        {
            UtilityIssueTags = new HashSet<UtilityIssueTag>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public virtual ICollection<UtilityIssueTag> UtilityIssueTags { get; set; }
    }
}
