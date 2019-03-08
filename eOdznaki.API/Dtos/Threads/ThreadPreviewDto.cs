using System;

namespace eOdznaki.Dtos.Threads
{
    public class ThreadPreviewDto
    {
        public int Id { get; private set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; private set; }
    }
}
