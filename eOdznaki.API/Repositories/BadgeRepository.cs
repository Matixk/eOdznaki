using eOdznaki.Models.Badges;
using eOdznaki.Persistence;
using eOdznaki.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace eOdznaki.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly DataContext context;
        private readonly ILogger<BadgeRepository> logger;

        public BadgeRepository(DataContext context, ILogger<BadgeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Badge AddBadge(Badge newBadge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Badge> GetAllBadges()
        {
            logger.LogInformation("GetAllBadges was called");
            return context.Badges.ToList();
        }

        public IEnumerable<Badge> GetBadgesByType(string type)
        {
            logger.LogInformation($"GetBadgesByType was called with parameter {type}");
            // Only an example, create a switch statement checking type
            return context.Badges.OfType<BadgeDrops>().ToList();
        }
    }
}
