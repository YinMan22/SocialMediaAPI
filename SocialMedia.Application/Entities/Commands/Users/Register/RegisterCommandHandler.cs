using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Application.Entities.Commands.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(ISocialMediaDbContext context, ILogger<RegisterCommandHandler> logger, IPasswordHasher passwordHasher)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start Register | Request={JsonConvert.SerializeObject(request)}");

                RegisterResponse response = new();

                // Validate username not exist
                bool userExist = await _context.Users.AnyAsync(u => u.Username.Equals(request.Username), cancellationToken);

                if (!userExist)
                {
                    // Password Hashing
                    string hashedPassword = _passwordHasher.Hash(request.Password);

                    User user = new()
                    {
                        Username = request.Username,
                        HashedPassword = hashedPassword,
                        FullName = request.FullName
                    };

                    await _context.Users.AddAsync(user, cancellationToken);
                    await _context.SaveChangesAsync();

                    return new RegisterResponse()
                    {
                        IsSuccess = true,
                        ResultCode = ResultCodes.Success,
                        Message = ResultMessage.RegisterSuccessful,
                    };
                }
                else
                    throw new BadRequestException(ResultCodes.DuplicateUsername, ResultMessage.UsernameExists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Register Exception: {ex.Message} | Request={JsonConvert.SerializeObject(request)}");
                throw;
            }
        }
    }
}
