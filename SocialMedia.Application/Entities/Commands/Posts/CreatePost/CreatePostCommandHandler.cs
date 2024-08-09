using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Application.Entities.Commands.Posts.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly ILogger<CreatePostCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePostCommandHandler(ISocialMediaDbContext context, ILogger<CreatePostCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreatePostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start CreatePost | Request={JsonConvert.SerializeObject(request)}");

                CreatePostResponse result = new();
                int userId = GetUserId();

                if (userId <= 0)
                    throw new BadRequestException(ResultCodes.TokenIssue, ResultMessage.TokenIssue);

                byte[] bytes = Encoding.ASCII.GetBytes(request.Image);

                Post post = new()
                {
                    UserId = userId,
                    Content = request.Content,
                    Image = bytes,
                    CreatedTime = DateTime.UtcNow,
                };

                await _context.Posts.AddAsync(post, cancellationToken);
                await _context.SaveChangesAsync();

                return new CreatePostResponse
                {
                    IsSuccess = true,
                    ResultCode = ResultCodes.Success,
                    Message = ResultMessage.CreatePostSuccessful
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreatePost Exception: {ex.Message} | Request={JsonConvert.SerializeObject(request)}");
                throw;
            }
        }

        private int GetUserId()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));

            return userId;
        }
    }
}
