using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Entities.Queries.Posts.GetPosts;
using SocialMedia.Application.Entities.Commands.Posts.CreatePost;
using SocialMedia.Application.Entities.Commands.Posts.LikeOrUnlikePost;
using SocialMedia.Application.Entities.Queries.Posts.GetLikePosts;
using SocialMedia.Application.Entities.Commands.Posts.CommentPost;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<GetPostsResponse>> Get([FromQuery] GetPostsQuery query, CancellationToken cancellationToken)
    {
        GetPostsResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreatePostResponse>> Create([FromBody] CreatePostCommand query, CancellationToken cancellationToken)
    {
        CreatePostResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("LikeOrUnlike")]
    public async Task<ActionResult<LikeOrUnlikePostResponse>> LikeOrUnlike([FromBody][Required] LikeOrUnlikePostCommand query, CancellationToken cancellationToken)
    {
        LikeOrUnlikePostResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("Comment")]
    public async Task<ActionResult<CommentPostResponse>> Comment([FromBody][Required] CommentPostCommand query, CancellationToken cancellationToken)
    {
        CommentPostResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("GetLikePost")]
    public async Task<ActionResult<GetLikePostsResponse>> GetLikePost(CancellationToken cancellationToken)
    {
        GetLikePostsResponse result = await Mediator.Send(new GetLikePostsQuery());
        return Ok(result);
    }
}
