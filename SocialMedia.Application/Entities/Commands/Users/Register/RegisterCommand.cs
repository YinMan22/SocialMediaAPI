using MediatR;

namespace SocialMedia.Application.Entities.Commands.Users.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
