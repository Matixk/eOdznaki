using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class Post
    {
        public int Id { get; private set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime Created { get; private set; }

        [Required]
        public User Author { get; set; }
    }
}