using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Announcements
{
    public class AnnouncementForCreateDto
    {
        [Required] [MaxLength(50)] public string Title { get; set; }

        [Required] [MaxLength(500)] public string Content { get; set; }

        [Required] public int AuthorId { get; set; }

        public DateTime Created { get; private set; }

        [Required] public DateTime Expiration { get; set; }
    }
}