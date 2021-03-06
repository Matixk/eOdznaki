﻿using eOdznaki.Models.Badges;
using eOdznaki.Models.Trails;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace eOdznaki.Models.Locations
{
    public class Location : GeoCoordinate
    {
        public Location()
        {}

        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public virtual ICollection<Trail> Trails { get; set; }

        public virtual ICollection<BadgeSummit> BadgesSummit { get; set; }
    }
}