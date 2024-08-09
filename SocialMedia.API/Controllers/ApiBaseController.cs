using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiBaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
