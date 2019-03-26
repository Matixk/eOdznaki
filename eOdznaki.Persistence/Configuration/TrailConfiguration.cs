using eOdznaki.Models.Locations;
using eOdznaki.Models.Trails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    class TrailConfiguration : IEntityTypeConfiguration<Trail>
    {
        public void Configure(EntityTypeBuilder<Trail> builder)
        {
            builder.HasOne(t => t.StartPoint).WithOne().HasForeignKey<Location>(l => l.Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.EndPoint).WithOne().HasForeignKey<Location>(l => l.Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Checkpoints).WithOne().HasForeignKey(l => l.Id).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
