namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BicycleParking")]
    public partial class BicycleParking
    {
        public BicycleParking()
        {
            BicycleParkingVotes = new HashSet<BicycleParkingVote>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(150)]
        public string PhotoUrl { get; set; }

        public bool Exist { get; set; }

        public int Type { get; set; }

        public bool Lock { get; set; }

        public bool Camera { get; set; }

        public bool Rent { get; set; }

        public int Quality { get; set; }

        public int Capacity { get; set; }

        public int VotesCount { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Address { get; set; }

        public double CenterDistance { get; set; }

        public virtual City City { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<BicycleParkingVote> BicycleParkingVotes { get; set; }
    }
}
