using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class Badge
    {
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public DateTime Acquired { get; set; }
        [Required]
        public string BadgeStatus { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
