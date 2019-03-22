using eOdznaki.Interfaces;
using eOdznaki.Models.Badges;
using System;

namespace eOdznaki.Services
{
    class BadgeAuthorizer : IBadgeAuthorizer
    {
        private readonly IBadgeRepository repository;

        public BadgeAuthorizer(IBadgeRepository repository)
        {
            this.repository = repository;
        }

        // Before calling this method you need to check if badge hasn't reached max level already
        public void CheckDropsBadge(BadgeDrops badge)
        {
            var requirements = repository.GetBadgeRequirements(badge.Id, badge.BadgeLevel);

            if (requirements == null) return;

            if (requirements.Requirement < badge.ReachedHeight)
            {
                repository.UpdateBadgeLevel(badge.Id, badge.BadgeLevel + 1, BadgeTypeEnum.BadgeDrop);
                repository.ResetBadgeReachedHeigh(badge.Id);
            }
        }

        public void CheckSummitBadge(BadgeSummit badge)
        {
            if (badge.UnreachedSummits != null)
            {
                repository.UpdateBadgeStatus(badge.Id, "Achieved");
            }
        }

        public void CheckTrailsBadge(BadgeTrails badge)
        {
            var requirements = repository.GetBadgeRequirements(badge.Id, badge.BadgeLevel);

            if (requirements == null) return;

            if (requirements.Requirement < badge.PointsAquired)
            {
                repository.UpdateBadgeLevel(badge.Id, badge.BadgeLevel + 1, BadgeTypeEnum.BadgeTrail);
            }
        }
    }
}
