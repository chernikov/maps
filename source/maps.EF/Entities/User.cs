using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.EF.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        public string AvatarPath { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Role> Roles { get; set; }

        public List<Social> Socials { get; set; }

        public List<Goal> Goals { get; set; }

        public List<BicycleParkingVote> BicycleParkingVotes { get; set; }

        public List<UtilityIssue> UtilityIssues { get; set; }

        public List<UtilityIssueHistory> UtilityIssueHistories { get; set; }

        public List<UtilityPhoto> UtilityPhotos { get; set; }

        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                var hasRole = Roles.Any(p => string.Compare(p.Code, role, true) == 0);
                if (hasRole)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSocial(Social.ProviderType type)
        {
            return Socials.Any(p => p.Provider == (int)type);
        }

        public bool HasBicycleParkingVote(BicycleParking bicycleParking)
        {
            if (BicycleParkingVotes != null && bicycleParking != null)
            {
                var exist = BicycleParkingVotes.Any(p => p.BicycleParking.Id == bicycleParking.Id);
                return exist;
            }
            return false;
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
