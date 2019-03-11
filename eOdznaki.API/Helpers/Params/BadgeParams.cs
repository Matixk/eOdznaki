using System;
using eOdznaki.Models.Badges;

namespace eOdznaki.Helpers.Params
{
    public class BadgeParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = Math.Min(MaxPageSize, value);
        }

        // public BadgeTypeEnum Type { get; set; }
    }
}