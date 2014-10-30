using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class GoalCell
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int State { get; set; }

        public DateTime AddedDate { get; set; }
	}
}