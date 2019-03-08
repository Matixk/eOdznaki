using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumThreads
{
    public class ForumThreadForCreateDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
