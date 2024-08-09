using AutoMapper;
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

namespace SocialMedia.Application.Entities.Commands.Posts.LikeOrUnlikePost
{
    public class LikeOrUnlikePostCommandHandler : IRequestHandler<LikeOrUnlikePostCommand, LikeOrUnlikePostResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly ILogger<LikeOrUnlikePostCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeOrUnlikePostCommandHandler(ISocialMediaDbContext context, ILogger<LikeOrUnlikePostCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LikeOrUnlikePostResponse> Handle(LikeOrUnlikePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start LikeOrUnlikePost | Request={JsonConvert.SerializeObject(request)}");

                LikeOrUnlikePostResponse result = new();
                int userId = GetUserId();

                if (userId <= 0)
                    throw new BadRequestException(ResultCodes.TokenIssue, ResultMessage.TokenIssue);

                Post? post = await _context.Posts
                                .Where(p => p.Id == request.PostId)
                                .FirstOrDefaultAsync(cancellationToken);

                if (post is null)
                    throw new BadRequestException(ResultCodes.IdValueInvalid, $"PostId: {request.PostId} is not found.");

                LikePost? likePost = await _context.LikePosts
                                .Where(u => u.UserId == userId && u.PostId == post.Id)
                                .FirstOrDefaultAsync(cancellationToken);

                if (likePost != null)
                    UpdateExistingLikePost(post, likePost);
                else
                    await AddNewLikePostAsync(userId, post, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return new LikeOrUnlikePostResponse
                {
                    IsSuccess=true,
                    ResultCode = ResultCodes.Success,
                    Message = ResultMessage.LikeOrUnlikePostSuccessful,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"LikeOrUnlikePost Exception: {ex.Message} | Request={JsonConvert.SerializeObject(request)}");
                throw;
            }
        }

        private int GetUserId()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));

            return userId;
        }

        private void UpdateExistingLikePost(Post post, LikePost likePost)
        {
            bool updatedLike = !likePost.Islike;

            likePost.Islike = updatedLike;
            likePost.CreatedTime = DateTime.UtcNow;

            post.Likes += updatedLike ? 1 : -1;
            if (post.Likes < 0)
                post.Likes = 0;
        }

        private async Task AddNewLikePostAsync(int userId, Post post, CancellationToken cancellationToken)
        {
            // Insert like post history
            LikePost likePostRecord = new LikePost
            {
                UserId = userId,
                PostId = post.Id,
                Islike = true,
                CreatedTime = DateTime.UtcNow,
            };
            await _context.LikePosts.AddAsync(likePostRecord, cancellationToken);

            // Update post's like
            post.Likes += 1;
        }
    }
}
