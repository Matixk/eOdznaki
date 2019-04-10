using eOdznaki.Models.Badges;

namespace eOdznaki.Interfaces
{
    public interface IBadgeAuthorizer
    {
        void CheckSummitBadge(BadgeSummit badge);
        void CheckDropsBadge(BadgeDrops badge);
        void CheckTrailsBadge(BadgeTrails badge);
    }
}
