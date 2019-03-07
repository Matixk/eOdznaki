using eOdznaki.Models.Badges;
using System.Collections.Generic;

namespace eOdznaki.Persistence.Repositories
{
    public interface IBadgeRepository
    {
        IEnumerable<Badge> GetAllBadges();
        IEnumerable<Badge> GetBadgesByType(string type);

        Badge AddBadge(Badge newBadge);
    }
}
