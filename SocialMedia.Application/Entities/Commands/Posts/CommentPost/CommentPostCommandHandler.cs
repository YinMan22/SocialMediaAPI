using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using System.Security.Claims;

namespace SocialMedia.Application.Entities.Commands.Posts.CommentPost
{
    public class CommentPostCommandHandler: IRequestHandler<CommentPostCommand, CommentPostResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly ILogger<CommentPostCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentPostCommandHandler(ISocialMediaDbContext context, ILogger<CommentPostCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommentPostResponse> Handle(CommentPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start CommentPost | Request={JsonConvert.SerializeObject(request)}");

                CommentPostResponse result = new();
                int userId = GetUserId();

                if (userId <= 0)
                    throw new BadRequestException(ResultCodes.TokenIssue, ResultMessage.TokenIssue);

                Post? post = await _context.Posts
                                .Where(p => p.Id == request.PostId)
                                .FirstOrDefaultAsync(cancellationToken);

                if (post is null)
                    throw new BadRequestException(ResultCodes.IdValueInvalid, $"PostId: {request.PostId} is not found.");

                Comment comment = new()
                {
                    UserId = userId,
                    PostId = request.PostId,
                    Text = request.Comment,
                    CreatedTime = DateTime.UtcNow,
                };

                await _context.Comments.AddAsync(comment, cancellationToken);
                await _context.SaveChangesAsync();

                return new CommentPostResponse
                {
                    IsSuccess = true,
                    ResultCode = ResultCodes.Success,
                    Message = ResultMessage.CommentSuccessful
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CommentPost Exception: {ex.Message} | Request={JsonConvert.SerializeObject(request)}");
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
