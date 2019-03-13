using eOdznaki.Models.Badges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    class BadgeDropsConfiguration : IEntityTypeConfiguration<BadgeDrops>
    {
        public void Configure(EntityTypeBuilder<BadgeDrops> builder)
        {
        }
    }
}
