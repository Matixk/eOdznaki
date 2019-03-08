using eOdznaki.Models.Badges;
using eOdznaki.Persistence;
using eOdznaki.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Badge> AddBadge(Badge newBadge)
        {
            await context.AddAsync(newBadge);
            await SaveAll();
            return newBadge;
        }

        public async Task<IEnumerable<Badge>> GetAllBadges()
        {
            logger.LogInformation("GetAllBadges was called");
            return await context.Badges.ToListAsync();
        }

        public async Task<Badge> GetBadgeByType(BadgeTypeEnum type, int badgeId)
        {
            logger.LogInformation($"GetBadgeByType was called with parameter {type}");

            switch (type)
            {
                case BadgeTypeEnum.BadgeDrop:
                    return await context.Badges.OfType<BadgeDrops>().FirstOrDefaultAsync(b => b.Id == badgeId);

                case BadgeTypeEnum.BadgeSummit:
                    return await context.Badges.OfType<BadgeSummit>().FirstOrDefaultAsync(b => b.Id == badgeId);

                case BadgeTypeEnum.BadgeTrail:
                    return await context.Badges.OfType<BadgeTrails>().FirstOrDefaultAsync(b => b.Id == badgeId);

                default:
                    logger.LogWarning($"Couldn't find appropriate type for {type}");
                    return new Badge();
            }
        }

        public async Task<IEnumerable<Badge>> GetBadgesByType(BadgeTypeEnum type)
        {
            logger.LogInformation($"GetBadgesByType was called with parameter {type}");

            switch (type)
            {
                case BadgeTypeEnum.BadgeDrop:
                    return await context.Badges.OfType<BadgeDrops>().ToListAsync();

                case BadgeTypeEnum.BadgeSummit:
                    return await context.Badges.OfType<BadgeSummit>().ToListAsync();

                case BadgeTypeEnum.BadgeTrail:
                    return await context.Badges.OfType<BadgeTrails>().ToListAsync();

                default:
                    logger.LogWarning($"Couldn't find appropriate type for {type}");
                    return null;
            }
        }

        public async Task SaveAll()
        {
            logger.LogInformation("SaveDatabaseInformation was called");
            await context.SaveChangesAsync();
        }

        public Task<Badge> UpdateBadgeData(Badge updatedBadge, BadgeTypeEnum type)
        {
            throw new NotImplementedException();
        }

        public async Task<Badge> UpdateBadgeLevelAsync(int badgeId, int newBadgeLevel, BadgeTypeEnum type)
        {
            if (type == BadgeTypeEnum.BadgeDrop)
            {
                var badge = (BadgeDrops) await context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);
                badge.BadgeLevel = newBadgeLevel;
                await SaveAll();
                return badge;
            }
            else
            {
                var badge = (BadgeTrails) await context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);
                badge.BadgeLevel = newBadgeLevel;
                await SaveAll();
                return badge;
            }
        }

        public async Task<Badge> UpdateBadgeStatus(int badgeId, string newBadgeStatus)
        {
            var badge = await context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);
            badge.BadgeStatus = newBadgeStatus;
            await SaveAll();
            return badge;
        }
    }
}
