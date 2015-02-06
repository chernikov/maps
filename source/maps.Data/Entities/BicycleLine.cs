namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BicycleLine")]
    public partial class BicycleLine
    {
        public BicycleLine()
        {
            BicycleDirectionLines = new HashSet<BicycleDirectionLine>();
        }

        public int ID { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(150)]
        public string Start { get; set; }

        [Required]
        [StringLength(150)]
        public string End { get; set; }

        public double StartLat { get; set; }

        public double StartLng { get; set; }

        public double EndLat { get; set; }

        public double EndLng { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<BicycleDirectionLine> BicycleDirectionLines { get; set; }

        public virtual City City { get; set; }
    }
}
