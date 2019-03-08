using eOdznaki.Models.Badges;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eOdznaki.Persistence.Repositories
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<Badge>> GetAllBadges();
        Task<IEnumerable<Badge>> GetBadgesByType(BadgeTypeEnum type);
        Task<Badge> GetBadgeByType(BadgeTypeEnum type, int badgeId);

        Task AddBadge(Badge newBadge);

        Task SaveAll();
    }
}
