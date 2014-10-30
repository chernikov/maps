using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class BycicleDirection
    {
        public int Id { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public User User { get; set; }

        public List<BicycleLine> BicycleLines { get; set; }

        public string Waypoints { get; set; }

        public string PolyLine { get; set; }

        public double Length { get; set; }

        public bool Processed { get; set; }

        public DateTime AddedDate { get; set; }

	}
}