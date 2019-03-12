using System;

namespace eOdznaki.Helpers.Params
{
    public class AnnouncementsParams
    {
        private const int MaxPageSize = 5;
        private int pageSize = 1;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = Math.Min(MaxPageSize, value);
        }
    }
}