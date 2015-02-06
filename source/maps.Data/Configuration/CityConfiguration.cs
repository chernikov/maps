using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            HasMany(e => e.BicycleLines)
              .WithRequired(e => e.City)
              .WillCascadeOnDelete(false);

            HasMany(e => e.BicycleParkings)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            HasMany(e => e.BycicleDirections)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            HasMany(e => e.InstagramPhotoes)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Users)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            HasMany(e => e.UtilityDepartments)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            HasMany(e => e.UtilityIssues)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

        }
    }
}
