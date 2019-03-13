using eOdznaki.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    public class ForumThreadConfiguration : IEntityTypeConfiguration<ForumThread>
    {
        public void Configure(EntityTypeBuilder<ForumThread> builder)
        {
            builder.HasKey(p => new {p.AuthorId});

            builder.HasOne(p => p.Author)
                .WithMany(u => u.UserForumThreads)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}