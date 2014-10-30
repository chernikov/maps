using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.EF.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Code;

        [Required]
        [StringLength(30)]
        public string Name;
    }
}
