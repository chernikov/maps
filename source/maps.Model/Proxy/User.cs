﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class User
    {
        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                var hasRole = UserRoles.Any(p => string.Compare(p.Role.Code, role, true) == 0);
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
            if (bicycleParking != null)
            {
                var exist = BicycleParkingVotes.Any(p => p.BicycleParkingID == bicycleParking.ID);
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

        public bool HasVisualizationAccess(int id)
        {
            return Visualizations.Any(p => p.ID == id) || VisualizationUsers.Any(p => p.VisualizationID == id);
        }
    }
}
