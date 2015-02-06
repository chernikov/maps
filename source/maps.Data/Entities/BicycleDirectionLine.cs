namespace maps.Data.Entities
{
    using maps.Data.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BicycleDirectionLine")]
    public partial class BicycleDirectionLine : DatabaseEntity
    {
        public int ID { get; set; }

        public int BicycleDirectionID { get; set; }

        public int BicycleLineID { get; set; }

        public virtual BicycleDirection BycicleDirection { get; set; }

        public virtual BicycleLine BicycleLine { get; set; }
    }
}
