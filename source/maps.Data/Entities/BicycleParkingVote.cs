namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BicycleParkingVote")]
    public partial class BicycleParkingVote
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int BicycleParkingID { get; set; }

        public int Mark { get; set; }

        public virtual BicycleParking BicycleParking { get; set; }

        public virtual User User { get; set; }
    }
}
