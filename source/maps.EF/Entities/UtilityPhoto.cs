using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityPhoto
    {
        public enum Type
        {
            Problem = 0x01,
            Process = 0x02,
            Resolve = 0x03
        }

        public int Id { get; set; }

        public UtilityIssue UtilityIssue { get; set; }

        public User User { get; set; }

        public string Image { get; set; }

        public DateTime AddedDate { get; set; }

        public int State { get; set; }

        public bool IsRemoved { get; set; }
      
	}
}