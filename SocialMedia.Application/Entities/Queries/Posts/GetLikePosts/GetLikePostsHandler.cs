using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Extensions;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Paginations;
using SocialMedia.Application.Entities.Commands.Users.Register;
using System.Security.Claims;
using static SocialMedia.Application.Entities.Queries.Posts.GetLikePosts.GetLikePostsResponse;

namespace SocialMedia.Application.Entities.Queries.Posts.GetLikePosts
{
    public class GetLikePostsHandler : IRequestHandler<GetLikePostsQuery, GetLikePostsResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<GetLikePostsHandler> _logger;

        public GetLikePostsHandler(ISocialMediaDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetLikePostsHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<GetLikePostsResponse> Handle(GetLikePostsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetLikePosts | Request={JsonConvert.SerializeObject(request)}");

            int userId = GetUserId();

            if (userId <= 0)
                throw new BadRequestException(ResultCodes.TokenIssue, ResultMessage.TokenIssue);

            PaginatedList<LikePostDto> likePostDto = await _context.LikePosts
                            .Include(l => l.Post)
                                .ThenInclude(p => p.User)
                            .Where(x => x.UserId == userId && x.Islike == true)
                            .ProjectTo<LikePostDto>(_mapper.ConfigurationProvider)
                            .ToPaginatedListAsync(request.PageIndex.Value, request.PageSize.Value, cancellationToken);

            if (likePostDto.Item.Any())
                return new GetLikePostsResponse(likePostDto, true, ResultCodes.Success, ResultMessage.GetLikePostsSuccess);
            else
                return new GetLikePostsResponse(likePostDto, true, ResultCodes.ListEmpty, ResultMessage.LikePostsListEmpty);
        }

        private int GetUserId()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));

            return userId;
        }
    }
}
