using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class BicycleParking
    {
        public enum TypeEnum : int
        {
            Wheel = 0x01,
            Frame = 0x02
        }

        public string TypeStr
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.Wheel :
                        return "колесо";
                    case TypeEnum.Frame:
                        return "рама";
                }
                return string.Empty;
            }
        }

        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public City City { get; set; }

        public List<BicycleParkingVote> BicycleParkingVotes { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        public string PhotoUrl { get; set; }

        public bool Exist { get; set;  }

        public int Type { get; set; }

        public bool Lock { get; set; }

        public bool Camera { get; set;  }

        public bool Rent { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Address { get; set; }

        public double CenterDistance { get; set; }
	}
}