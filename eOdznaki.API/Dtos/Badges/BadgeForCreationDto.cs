using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos
{
    public class BadgeForCreationDto
    {
        [Required] public string Name { get; private set; }
    }
}