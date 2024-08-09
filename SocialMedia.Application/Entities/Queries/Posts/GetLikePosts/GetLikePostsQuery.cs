using MediatR;
using SocialMedia.Application.Common.Paginations;

namespace SocialMedia.Application.Entities.Queries.Posts.GetLikePosts
{
    public class GetLikePostsQuery : PaginationQuery, IRequest<GetLikePostsResponse>
    {
    }
}
