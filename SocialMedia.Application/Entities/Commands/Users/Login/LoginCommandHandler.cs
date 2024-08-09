using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Application.Entities.Commands.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _config;

        public LoginCommandHandler(ISocialMediaDbContext context, ILogger<LoginCommandHandler> logger, 
            IPasswordHasher passwordHasher, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _config = config;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                _logger.LogInformation($"Start Login | Request={JsonConvert.SerializeObject(request)}");

                // Retrieve the user record
                User? user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);

                if (user is null)
                    throw new BadRequestException(ResultCodes.InvalidCredentials, ResultMessage.InvalidCredentials);

                bool isVerify = _passwordHasher.Verify(request.Password, user.HashedPassword);

                if (isVerify) 
                {
                    // Create JWT
                    string token = CreateJwt(user);

                    return new LoginResponse
                    {
                        JWT = token,
                        IsSuccess = true,
                        ResultCode = ResultCodes.Success,
                        Message = ResultMessage.LoginSuccessful,
                    };
                }
                else
                    throw new BadRequestException(ResultCodes.InvalidCredentials, ResultMessage.InvalidCredentials);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Login Exception: {ex.Message} | Request={JsonConvert.SerializeObject(request)}");
                throw;
            }
        }

        private string CreateJwt(User user)
        {
            var claims = new [] { 
                new Claim("UserId", user.Id.ToString()) 
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("JwtSettings").GetValue<string>("Key")));

            SigningCredentials credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.UtcNow.AddMinutes(60), 
                signingCredentials: credential
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
