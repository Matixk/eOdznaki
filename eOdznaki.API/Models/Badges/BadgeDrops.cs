using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeDrops : Badge
    {
        [Required] public int BadgeLevel { get; set; }

        [Required] public int MaxLevel { get; private set; }

        [Required] public int ReachedHeight { get; set; }
    }
}