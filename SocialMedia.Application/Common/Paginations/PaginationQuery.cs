namespace SocialMedia.Application.Common.Paginations
{
    public abstract class PaginationQuery
    {
        private const int DEFAULT_PAGE_SIZE = 15;
        private const int DEFAULT_PAGE_INDEX = 1;
        private int _pageSize = DEFAULT_PAGE_SIZE;
        private int _pageIndex = DEFAULT_PAGE_INDEX;


        public int? PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value.HasValue && value.Value > 0 ? value.Value : DEFAULT_PAGE_INDEX;
        }

        public int? PageSize
        {
            get => _pageSize;
            set => _pageSize = value.HasValue && value.Value > 0 ? value.Value : DEFAULT_PAGE_SIZE;
        }
    }
}
