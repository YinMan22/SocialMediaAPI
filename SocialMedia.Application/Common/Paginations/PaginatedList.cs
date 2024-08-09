namespace SocialMedia.Application.Common.Paginations
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasPreviousPage => PageIndex > 1;
        public List<T> Item { get; }
        public IReadOnlyList<T>? Items { get; internal set; }

        public PaginatedList(List<T> items, int totalCount, int totalPages, int pageIndex) 
        {
            Item = items;
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageIndex = totalCount > 0 ? pageIndex : 0;
        }
    }
}
