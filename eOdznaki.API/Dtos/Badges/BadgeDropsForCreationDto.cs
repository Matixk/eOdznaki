using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos
{
    public class BadgeDropsForCreationDto : BadgeForCreationDto
    {
        [Required]
        public int MaxLevel { get; private set; }
    }
}
