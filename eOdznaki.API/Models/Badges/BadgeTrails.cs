using eOdznaki.Models.Trails;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeTrails
    {
        [Required]
        public int BadgeLevel { get; set; }
        [Required]
        public int PointsAquired { get; set; }
        [Required]
        public IDictionary<int, int> LevelRequirements { get; private set; }
        [Required]
        public IEnumerable<int> Requirements { get; private set; }
        [Required]
        public IEnumerable<Trail> Trails { get; private set; }
    }
}
