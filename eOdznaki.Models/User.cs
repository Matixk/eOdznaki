using System;
using System.Collections.Generic;
using eOdznaki.Models.Badges;
using Microsoft.AspNetCore.Identity;

namespace eOdznaki.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string AvatarUrl { get; set; }
        public string AvatarPublicKey { get; set; }

        public IEnumerable<Badge> Badges { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<ForumThread> UserForumThreads { get; set; }
        public IEnumerable<ForumPost> UserForumPosts { get; set; }
        public IEnumerable<Announcement> UserAnnouncements { get; set; }
        public DateTime Created { get; set; }
    }
}