using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostForUpdateDto
    {
        [Required]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }
    }
}