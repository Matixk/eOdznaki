using System;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostPreviewDto
    {
        public int Id { get; private set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}