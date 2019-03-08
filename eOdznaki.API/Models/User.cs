using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace eOdznaki.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
//      public IEnumerable<Badge> Badges { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<ForumThread> UserForumThreads { get; set; }
        public IEnumerable<ForumPost> UserForumPosts { get; set; }
    }
}