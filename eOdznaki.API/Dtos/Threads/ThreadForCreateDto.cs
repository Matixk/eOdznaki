using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Threads
{
    public class ThreadForCreateDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
