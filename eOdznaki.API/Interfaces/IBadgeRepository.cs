using eOdznaki.Models.Badges;
using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;

namespace eOdznaki.Persistence.Repositories
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<Badge>> GetAllBadges();
        Task<IEnumerable<Badge>> GetBadgesByType(BadgeTypeEnum type);
        Task<Badge> GetBadgeById(int badgeId);

        Task<Badge> AddBadge(Badge newBadge);
        Task<Badge> UpdateBadgeLevel(int badgeId, int newBadgeLevel, BadgeTypeEnum type);
        Task<Badge> UpdateBadgeData(Badge updatedBadge);
        Task<Badge> UpdateBadgeStatus(int badgeId, string newBadgeStatus);
        Task<bool> DeleteBadgeById(int id);

        Task<bool> SaveAll();
    }
}
