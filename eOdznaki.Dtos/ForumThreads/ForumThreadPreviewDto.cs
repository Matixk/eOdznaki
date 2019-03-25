using System;

namespace eOdznaki.Dtos.ForumThreads
{
    public class ForumThreadPreviewDto
    {
        public int Id { get; private set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}