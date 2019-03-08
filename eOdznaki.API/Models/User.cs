using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace eOdznaki.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string AvatarUrl { get; set; }
//      public IEnumerable<Badge> Badges { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<Thread> UserThreads { get; set; }
        public IEnumerable<Post> UserPosts { get; set; }
    }
}