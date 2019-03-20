using System;
using System.Collections.Generic;
using eOdznaki.Models;

namespace eOdznaki.Dtos.Users
{
    public class UserForViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string AvatarUrl { get; set; }
        public string AvatarPublicKey { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

//      public IEnumerable<Badge> Badges { get; set; }
        public IEnumerable<ForumThread> UserForumThreads { get; set; }
        public IEnumerable<ForumPost> UserForumPosts { get; set; }
    }
}