using System;
using eOdznaki.Dtos.Users;

namespace eOdznaki.Dtos.ForumThreads
{
    public class ForumThreadPreviewDto
    {
        public int Id { get; private set; }
        public UserBasicPreviewDto Author { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}