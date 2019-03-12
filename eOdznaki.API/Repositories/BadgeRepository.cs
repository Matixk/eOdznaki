using System;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models.Badges;
using eOdznaki.Persistence;
using eOdznaki.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<bool> DeleteBadgeById(int id)
        {
            var badge = await context.Badges.FirstOrDefaultAsync(b => b.Id == id);
            context.Remove(badge);
            return await SaveAll();
        }

        public async Task<PagedList<Badge>> GetAllBadges(BadgeParams badgeParams)
        {
            logger.LogInformation("GetAllBadges was called");


            var badges = context.Badges.AsQueryable();

            return await PagedList<Badge>.CreateAsync(badges, badgeParams.PageNumber, badgeParams.PageSize);
        }

        public IQueryable<Badge> GetBadgeQuery(BadgeTypeEnum type)
        {
            switch (type)
            {
                case BadgeTypeEnum.BadgeDrop:
                    return context.Badges.OfType<BadgeDrops>();
                case BadgeTypeEnum.BadgeTrail:
                    return context.Badges.OfType<BadgeTrails>();
                case BadgeTypeEnum.BadgeSummit:
                    return context.Badges.OfType<BadgeSummit>();
                default:
                    return null;
            }
        }

        public async Task<Badge> GetBadgeById(int badgeId)
        {
            return await context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);
        }

        public async Task<PagedList<Badge>> GetBadgesByType(BadgeParams badgeParams, BadgeTypeEnum type)
        {
            logger.LogInformation($"GetBadgesByType was called with parameter {type}");
            var badge = GetBadgeQuery(type);
            if (badge == null)
            {
                logger.LogError($"Badge type was not found: {type}");
                return null;
            }

            return await PagedList<Badge>.CreateAsync(badge, badgeParams.PageNumber, badgeParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            logger.LogInformation("SaveDatabaseInformation was called");
            return await context.SaveChangesAsync() > 0;
        }

        public Task<Badge> UpdateBadgeData(Badge updatedBadge)
        {
            throw new NotImplementedException();
        }

        public async Task<Badge> UpdateBadgeLevel(int badgeId, int newBadgeLevel, BadgeTypeEnum type)
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