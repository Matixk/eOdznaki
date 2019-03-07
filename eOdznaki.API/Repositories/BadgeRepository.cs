using eOdznaki.Models.Badges;
using eOdznaki.Persistence.Repositories;
using System;
using System.Collections.Generic;

namespace eOdznaki.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        public Badge AddBadge(Badge newBadge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Badge> GetAllBadges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Badge> GetBadgesByType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
