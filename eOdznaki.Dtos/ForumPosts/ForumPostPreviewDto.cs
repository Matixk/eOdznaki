using System;
using eOdznaki.Dtos.Users;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostPreviewDto
    {
        public int Id { get; private set; }
        public UserBasicPreviewDto Author { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}