using eOdznaki.Models.Locations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Trails
{
    public class Trail
    {
        public Trail()
        {}

        public Trail(Location origin, Location destination, IEnumerable<Location> checkpoints)
        {
            StartPoint = origin;
            EndPoint = destination;
            Checkpoints = checkpoints;
        }

        public Trail(Location origin, Location destination)
        {
            StartPoint = origin;
            EndPoint = destination;
        }

        public int Id { get; private set; }

        public Location StartPoint { get; private set; }

        public Location EndPoint { get; private set; }

        public IEnumerable<Location> Checkpoints { get; private set; }

        public int GOTPoints { get; private set; }
    }
}