using SocialMedia.Application.Common.Model;

namespace SocialMedia.Application.Entities.Commands.Posts.CreatePost
{
    public class CreatePostResponse : Response
    {
        public CreatePostResponse() : base(false, "", "")
        {
        }
    }
}
