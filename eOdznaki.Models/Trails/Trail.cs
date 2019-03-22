using eOdznaki.Models.Locations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models.Trails
{
    public class Trail
    {
        public int Id { get; private set; }

        [Required] public string Name { get; private set; }

        [Required] public Location StartPoint { get; private set; }

        [Required] public Location EndPoint { get; private set; }

        public IEnumerable<Location> Checkpoints { get; private set; }

        public int GOTPoints { get; private set; }
    }
}