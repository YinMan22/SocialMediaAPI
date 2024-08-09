using MediatR;

namespace SocialMedia.Application.Entities.Commands.Posts.CreatePost
{
    public class CreatePostCommand : IRequest<CreatePostResponse>
    {
        public string Content { get; set; }
        public string? Image { get; set; }
    }
}
