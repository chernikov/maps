using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public partial class BicycleParkingVote
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public BicycleParking BicycleParking { get; set; }

        public int Mark { get; set; }

	}
}