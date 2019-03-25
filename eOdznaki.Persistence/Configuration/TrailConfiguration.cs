using eOdznaki.Models.Trails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace eOdznaki.Persistence.Configuration
{
    class TrailConfiguration : IEntityTypeConfiguration<Trail>
    {
        public void Configure(EntityTypeBuilder<Trail> builder)
        {
            builder.HasOne(t => t.StartPoint).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.EndPoint).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Checkpoints).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
