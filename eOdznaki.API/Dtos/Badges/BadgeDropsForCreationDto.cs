using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Badges
{
    public class BadgeDropsForCreationDto : BadgeForCreationDto
    {
        [Required] public int MaxLevel { get; private set; }
    }
}