using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityDepartment
    {
        public int Id { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }


        public string Address { get; set; }
	}
}