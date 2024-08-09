using MediatR;

namespace SocialMedia.Application.Entities.Commands.Users.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
