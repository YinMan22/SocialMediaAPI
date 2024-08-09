using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Entities.Commands.Users.Register;
using SocialMedia.Application.Entities.Commands.Users.Login;
using SocialMedia.Application.Common.Model;
using SocialMedia.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ApiBaseController
{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody][Required] RegisterCommand query, CancellationToken cancellationToken)
    {
        RegisterResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody][Required] LoginCommand query, CancellationToken cancellationToken)
    {
        LoginResponse result = await Mediator.Send(query);
        return Ok(result);
    }
}
