using eOdznaki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Persistence
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Post>(post =>
            {
                post.HasKey(p => new {p.AuthorId, ThreadId = p.ForumThreadId});

                post.HasOne(p => p.Author)
                    .WithMany(u => u.UserPosts)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                post.HasOne(p => p.ForumThread)
                    .WithMany(t => t.Posts)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ForumThread>(post =>
            {
                post.HasKey(p => new { p.AuthorId });

                post.HasOne(p => p.Author)
                    .WithMany(u => u.UserForumThreads)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}