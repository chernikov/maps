using System;
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

        public bool IsActivated
        {
            get { return ActivatedDate.HasValue; }
        }

        public string FullAvatarPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AvatarPath))
                {
                    return "/Content/images/default_avatar.jpg?w=260&h=200&mode=crop";
                }
                return AvatarPath+"?w=260&h=200&mode=crop";
            }
        }
    }
}
