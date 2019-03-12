using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Announcements
{
    public class AnnouncementForUpdateDto
    {
        [Required] [MaxLength(50)] public string Title { get; set; }

        [Required] [MaxLength(500)] public string Content { get; set; }

        public DateTime Expiration { get; set; }
    }
}