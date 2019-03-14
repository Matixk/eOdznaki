using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Announcements
{
    public class AnnouncementForCreateDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }

        [Required] public int AuthorId { get; set; }

        public DateTime Created { get; private set; }

        [Required] public DateTime Expiration { get; set; }
    }
}