using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Threads
{
    public class ThreadForUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
