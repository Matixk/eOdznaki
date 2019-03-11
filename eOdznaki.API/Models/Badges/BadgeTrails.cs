using eOdznaki.Models.Trails;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeTrails : Badge
    {
        [Required]
        public int BadgeLevel { get; set; }
        [Required]
        public int MaxLevel { get; private set; }
        [Required]
        public int PointsAquired { get; set; }
        [Required]
        public IEnumerable<Trail> Trails { get; private set; }
    }
}
