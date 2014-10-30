using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityIssueHistory
    {
        public int Id { get; set; }

        [Required]
        public UtilityIssue UtilityIssue { get; set; }

        [Required]
        public User User { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? AcceptedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public UtilityDepartment UtilityDepartment { get; set; }

        public UtilityIssue MainUtilityIssue { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Solution { get; set; }

        public int Status { get; set; }

        public bool IsRemoved { get; set; }
	}
}