using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostForCreateDto
    {
        [Required] public int AuthorId { get; set; }

        [Required] public int ForumThreadId { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }
    }
}