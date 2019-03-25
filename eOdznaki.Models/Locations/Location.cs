using GeoCoordinatePortable;

namespace eOdznaki.Models.Locations
{
    public class Location : GeoCoordinate
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}