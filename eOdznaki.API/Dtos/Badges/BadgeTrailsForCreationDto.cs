using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eOdznaki.Models.Trails;

namespace eOdznaki.Dtos
{
    public class BadgeTrailsForCreationDto : BadgeForCreationDto
    {
        [Required] public int MaxLevel { get; private set; }

        [Required] public IEnumerable<Trail> Trails { get; private set; }
    }
}