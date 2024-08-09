using SocialMedia.Application.Common.Model;

namespace SocialMedia.Application.Entities.Commands.Posts.CommentPost
{
    public class CommentPostResponse : Response
    {
        public CommentPostResponse() : base(false, "", "")
        {
        }
    }
}
