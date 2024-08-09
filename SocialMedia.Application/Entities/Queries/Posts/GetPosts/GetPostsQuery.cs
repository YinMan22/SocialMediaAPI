using MediatR;
using SocialMedia.Application.Common.Paginations;

namespace SocialMedia.Application.Entities.Queries.Posts.GetPosts
{
    public class GetPostsQuery : PaginationQuery, IRequest<GetPostsResponse>
    {
        public int? UserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
