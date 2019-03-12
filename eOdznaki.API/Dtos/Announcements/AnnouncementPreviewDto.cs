using System;
using System.ComponentModel.DataAnnotations;
using eOdznaki.Models;

namespace eOdznaki.Dtos.Announcements
{
    public class AnnouncementPreviewDto
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int AuthorId { get; private set; }
        public string AuthorName { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Expiration { get; private set; }
    }
}
