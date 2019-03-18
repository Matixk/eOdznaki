using eOdznaki.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    public class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.HasOne(p => p.Author)
                .WithMany(u => u.UserForumPosts)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ForumThread)
                .WithMany(t => t.ForumPosts)
                .HasForeignKey(p => p.ForumThreadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}