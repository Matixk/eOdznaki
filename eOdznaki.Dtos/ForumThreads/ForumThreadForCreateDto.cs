using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumThreads
{
    public class ForumThreadForCreateDto
    {
        [Required] public int AuthorId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }
    }
}