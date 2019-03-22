using GeoCoordinatePortable;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Locations
{
    public class Location : GeoCoordinate
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        [Required] public GeoCoordinate Position { get; private set; }
    }
}