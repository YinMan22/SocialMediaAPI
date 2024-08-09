using MediatR;

namespace SocialMedia.Application.Entities.Commands.Posts.LikeOrUnlikePost
{
    public class LikeOrUnlikePostCommand : IRequest<LikeOrUnlikePostResponse>
    {
        public int PostId { get; set; }
    }
}
