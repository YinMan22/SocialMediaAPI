using MediatR;

namespace SocialMedia.Application.Entities.Commands.Posts.CommentPost
{
    public class CommentPostCommand : IRequest<CommentPostResponse>
    {
        public int PostId { get; set; }
        public string Comment {  get; set; }
    }
}
