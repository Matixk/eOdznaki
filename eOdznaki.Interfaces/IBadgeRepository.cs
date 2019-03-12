using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models.Badges;

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
        Task<Badge> UpdateBadgeData(Badge updatedBadge);
        Task<Badge> UpdateBadgeStatus(int badgeId, string newBadgeStatus);
        Task<bool> DeleteBadgeById(int id);

        Task<bool> SaveAll();
    }
}