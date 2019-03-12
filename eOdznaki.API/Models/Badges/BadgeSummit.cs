using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eOdznaki.Models.Locations;

namespace eOdznaki.Models.Badges
{
    public class BadgeSummit : Badge
    {
        [Required] public IEnumerable<Location> SummitLocations { get; private set; }
    }
}