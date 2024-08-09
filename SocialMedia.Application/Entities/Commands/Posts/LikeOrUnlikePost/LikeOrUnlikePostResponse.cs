using SocialMedia.Application.Common.Model;

namespace SocialMedia.Application.Entities.Commands.Posts.LikeOrUnlikePost
{
    public class LikeOrUnlikePostResponse : Response
    {
        public LikeOrUnlikePostResponse() : base(false, "", "")
        {
        }
    }
}
