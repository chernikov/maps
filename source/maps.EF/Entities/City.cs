using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public partial class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double CenterLat { get; set; }

        public double CenterLng { get; set; }

        public int Zoom { get; set; }
	}
}