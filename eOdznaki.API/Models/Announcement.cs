using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class Announcement
    {
        public int Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public DateTime Created { get; private set; }
        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public User Author { get; set; }

        public Announcement() { }

        public Announcement(string title, string content, int authorId, DateTime expiration, User author)
        {
            Title = title;
            Content = content;
            AuthorId = authorId;
            Created = DateTime.Now;
            Expiration = expiration;
            Author = author;
        }
    }
}
