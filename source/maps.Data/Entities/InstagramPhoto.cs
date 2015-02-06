namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstagramPhoto")]
    public partial class InstagramPhoto
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        public string GlobalId { get; set; }

        public int CityID { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public DateTime CreatedTime { get; set; }

        [Required]
        [StringLength(200)]
        public string PhotoUrl { get; set; }

        public string Caption { get; set; }

        public string Tags { get; set; }

        public long UserGlobalId { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string UserFullName { get; set; }

        public DateTime AddedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Link { get; set; }

        public virtual City City { get; set; }
    }
}
