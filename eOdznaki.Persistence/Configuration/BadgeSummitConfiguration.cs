﻿using eOdznaki.Models.Badges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    class BadgeSummitConfiguration : IEntityTypeConfiguration<BadgeSummit>
    {
        public void Configure(EntityTypeBuilder<BadgeSummit> builder)
        {
            builder.HasMany(b => b.ReachedSummits).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.UnreachedSummits).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
