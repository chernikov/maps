namespace maps.Data.Entities
{
    using maps.Data.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BycicleDirection")]
    public partial class BicycleDirection : DatabaseEntity
    {
        public BicycleDirection()
        {
            BicycleDirectionLines = new HashSet<BicycleDirectionLine>();
        }

        public int ID { get; set; }

        public int CityID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Waypoints { get; set; }

        [Required]
        public string PolyLine { get; set; }

        public double Length { get; set; }

        public bool Processed { get; set; }

        public DateTime AddedDate { get; set; }

        public virtual ICollection<BicycleDirectionLine> BicycleDirectionLines { get; set; }

        public virtual City City { get; set; }

        public virtual User User { get; set; }
    }
}
