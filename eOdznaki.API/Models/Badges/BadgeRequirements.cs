using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeRequirements
    {
        public int Id { get; private set; }
        [Required]
        public string BadgeId { get; private set; }
        [Required]
        public int BadgeLevel { get; private set; }
        [Required]
        public int Requirement { get; private set; }
    }
}
