using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public partial class Comment
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        public Comment Parent { get; set; }

        public DateTime AddedDate { get; set; }

        [Required]
        public string Text { get; set; }

        public List<Comment> Comments { get; set; }
	}
}