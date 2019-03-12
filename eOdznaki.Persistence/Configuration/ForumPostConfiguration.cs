using eOdznaki.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    public class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.HasKey(p => new {p.AuthorId, ThreadId = p.ForumThreadId});

            builder.HasOne(p => p.Author)
                .WithMany(u => u.UserForumPosts)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ForumThread)
                .WithMany(t => t.ForumPosts)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}