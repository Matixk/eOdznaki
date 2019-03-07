using eOdznaki.Models.Locations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Badges
{
    public class BadgeSummit : Badge
    {
        [Required]
        public IEnumerable<Location> SummitLocations { get; private set; };
    }
}
