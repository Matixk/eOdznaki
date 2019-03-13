using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Badges
{
    public class BadgeForCreationDto
    {
        [Required] public string Name { get; private set; }
    }
}