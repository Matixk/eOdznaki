using System;

namespace eOdznaki.Helpers.Params
{
    public class Params
    {
        protected int MaxPageSize;
        private int pageNumber = 1;
        private int pageSize;

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = Math.Max(1, value);
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = Math.Clamp(value, 1, MaxPageSize);
        }
    }
}