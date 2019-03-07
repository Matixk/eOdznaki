using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class Thread
    {
        public int Id { get; private set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime Created { get; private set; }

        [Required]
        public User Author { get; set; }

    }
}
