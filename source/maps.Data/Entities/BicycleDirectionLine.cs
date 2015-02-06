namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BicycleDirectionLine")]
    public partial class BicycleDirectionLine
    {
        public int ID { get; set; }

        public int BicycleDirectionID { get; set; }

        public int BicycleLineID { get; set; }

        public virtual BycicleDirection BycicleDirection { get; set; }

        public virtual BicycleLine BicycleLine { get; set; }
    }
}
