using eOdznaki.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eOdznaki.Persistence.Configuration
{
    public class ForumThreadConfiguration : IEntityTypeConfiguration<ForumThread>
    {
        public void Configure(EntityTypeBuilder<ForumThread> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.Author)
                .WithMany(u => u.UserForumThreads)
                .HasForeignKey(t => t.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.ForumPosts)
                .WithOne(p => p.ForumThread)
                .HasForeignKey(p => p.ForumThreadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}