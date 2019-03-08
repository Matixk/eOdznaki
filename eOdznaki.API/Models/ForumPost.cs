using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class ForumPost
    {
        public int Id { get; private set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int ForumThreadId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime Created { get; private set; }

        [Required]
        public User Author { get; set; }
        [Required]
        public ForumThread ForumThread { get; set; }
    }
}