using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models.Badges;
using eOdznaki.Models.Locations;

namespace eOdznaki.Interfaces
{
    public interface IBadgeRepository
    {
        Task<PagedList<Badge>> GetAllBadges(BadgeParams badgeParams);
        Task<PagedList<Badge>> GetBadgesByType(BadgeParams badgeParams, BadgeTypeEnum type);
        IQueryable<Badge> GetBadgeQuery(BadgeTypeEnum type);
        Task<Badge> GetBadgeById(int badgeId);

        Task<Badge> AddBadge(Badge newBadge);
        Task<Badge> UpdateBadgeLevel(int badgeId, int newBadgeLevel, BadgeTypeEnum type);
        Task<Badge> UpdateBadgeStatus(int badgeId, string newBadgeStatus);
        Task<Badge> UpdateBadgeDropsReachedHeight(int badgeId, int heightToAdd);
        Task<Badge> UpdateBadgeTrailsGOTPoints(int badgeId, int newGOTPoints);
        Task<Badge> UpdateBadgeSummitReachedSummits(int badgeId, Location summit);
        Task<Badge> ResetBadgeReachedHeigh(int badgeId);
        Task<bool> DeleteBadgeById(int id);

        BadgeRequirements GetBadgeRequirements(int badgeId, int badgeLevel);

        Task<bool> SaveAll();
    }
}