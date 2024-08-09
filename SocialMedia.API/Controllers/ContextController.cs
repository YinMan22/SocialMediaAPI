using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Entities.Queries.Context.GetCurrentUserContext;
using SocialMedia.Application.Entities.Queries.Posts.GetPosts;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContextController : ApiBaseController
{
    [HttpGet("GetCurrentUserContext")]
    public async Task<ActionResult<int>> Get()
    {
        int result = await Mediator.Send(new GetCurrentUserContextQuery());
        return Ok(result);
    }
}
