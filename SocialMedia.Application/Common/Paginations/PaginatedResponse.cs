using SocialMedia.Application.Common.Model;

namespace SocialMedia.Application.Common.Paginations
{
    public abstract class PaginatedResponse<T> : Response
    {
        public PaginatedResponse() : base(false, "", "")
        {
        }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public IReadOnlyList<T> Items { get; private set; }

        public PaginatedResponse(PaginatedList<T> paginatedItemList, bool isSuccess, string resultCode, string message) : base(isSuccess, resultCode, message)
        {
            Items = paginatedItemList.Item;
            PageIndex = paginatedItemList.PageIndex;
            TotalPages = paginatedItemList.TotalPages;
            TotalCount = paginatedItemList.TotalCount;
        }
    }
}
