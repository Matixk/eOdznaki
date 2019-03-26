using eOdznaki.Models.Locations;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Persistence.Configuration
{
    class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Location> builder)
        {
            builder.HasMany(l => l.BadgesSummit).WithOne().HasForeignKey(l => l.Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(l => l.Trails).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
