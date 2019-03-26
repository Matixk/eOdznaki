using eOdznaki.Models.Locations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos
{
    public class TrailDto
    {
        public Location StartPoint { get; private set; }

        public Location EndPoint { get; private set; }

        public IEnumerable<Location> Checkpoints { get; private set; }
    }
}
