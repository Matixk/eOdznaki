using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeDrops : Badge
    {
        [Required]
        public int BadgeLevel { get; set; }
        [Required]
        public IDictionary<int, IEnumerable<int>> LevelRequirements { get; private set; }
        [Required]
        public IEnumerable<int> Requirements { get; private set; }
    }
}
