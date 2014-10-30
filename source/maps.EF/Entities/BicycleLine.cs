using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class BicycleLine
    {
        public int Id { get; set; }

        [Required]
        public City City { get; set; }

        public List<BycicleDirection> BycicleDirections { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public double StartLat { get; set; }

        public double StartLng { get; set; }

        public double EndLat { get; set; }

        public double EndLng { get; set; }

        public int Quantity { get; set; }

      
	}
}