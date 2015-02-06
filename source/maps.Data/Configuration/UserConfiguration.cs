using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(e => e.BicycleParkingVotes)
              .WithRequired(e => e.User)
              .WillCascadeOnDelete(false);

            HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            HasMany(e => e.UtilityIssues)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            HasMany(e => e.UtilityIssueHistories)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
