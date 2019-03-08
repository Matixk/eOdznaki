using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostForCreateDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int ForumThreadId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
    }
}
