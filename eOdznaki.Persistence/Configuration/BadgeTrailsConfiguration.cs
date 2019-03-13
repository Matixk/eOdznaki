using eOdznaki.Models.Badges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    class BadgeTrailsConfiguration : IEntityTypeConfiguration<BadgeTrails>
    {
        public void Configure(EntityTypeBuilder<BadgeTrails> builder)
        {
        }
    }
}
