using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Paginations;

namespace SocialMedia.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var items = new List<T>();
            var totalPages = 0;

            var totalCount = await query.CountAsync(cancellationToken);

            if (totalCount > 0)
            {
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            }

            return new PaginatedList<T>(items, totalCount, totalPages, pageIndex);
        }
    }
}
