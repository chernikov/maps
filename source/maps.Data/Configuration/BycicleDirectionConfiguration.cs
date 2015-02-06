using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class BycicleDirectionConfiguration : EntityTypeConfiguration<BycicleDirection>
    {
        public BycicleDirectionConfiguration()
        {
           Property(e => e.Waypoints).IsUnicode(false);

           Property(e => e.PolyLine).IsUnicode(false);

           HasMany(e => e.BicycleDirectionLines)
                .WithRequired(e => e.BycicleDirection)
                .HasForeignKey(e => e.BicycleDirectionID);
        }
    }
}
