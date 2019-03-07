using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeLevel
    {
        public int Id { get; set; }
        [Required]
        public BadgeRequirements BadgeRequirements { get; }
    }
}
