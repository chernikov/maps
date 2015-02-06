namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Social")]
    public partial class Social
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Identified { get; set; }

        public int Provider { get; set; }

        public string UserInfo { get; set; }

        [Required]
        public string JsonResource { get; set; }

        public virtual User User { get; set; }
    }
}
