using eOdznaki.Models.Trails;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos
{
    public class BadgeTrailsForCreationDto : BadgeForCreationDto
    {
        [Required]
        public int MaxLevel { get; private set; }
        [Required]
        public IEnumerable<Trail> Trails { get; private set; }
    }
}
