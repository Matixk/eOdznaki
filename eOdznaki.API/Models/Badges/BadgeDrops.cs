using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeDrops : Badge
    {
        [Required]
        public BadgeLevel BadgeLevel { get; set; }
        [Required]
        public int MaxLevel { get; private set; }
    }
}
