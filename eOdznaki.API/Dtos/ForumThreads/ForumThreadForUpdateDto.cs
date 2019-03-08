using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumThreads
{
    public class ForumThreadForUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
