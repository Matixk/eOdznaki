using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Announcements
{
    public class AnnouncementForUpdateDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }

        public DateTime Expiration { get; set; }
    }
}