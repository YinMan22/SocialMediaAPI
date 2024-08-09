using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Extensions;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Paginations;
using SocialMedia.Application.Entities.Queries.Posts.GetLikePosts;
using static SocialMedia.Application.Entities.Queries.Posts.GetPosts.GetPostsResponse;

namespace SocialMedia.Application.Entities.Queries.Posts.GetPosts
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, GetPostsResponse>
    {
        private readonly ISocialMediaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPostsHandler> _logger;

        public GetPostsHandler(ISocialMediaDbContext context, IMapper mapper, ILogger<GetPostsHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetPostsResponse> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetPosts | Request={JsonConvert.SerializeObject(request)}");

            if (!request.DateFrom.HasValue && !request.DateFrom.HasValue)
            {
                request.DateFrom = DateTime.UtcNow.AddMonths(-1);
                request.DateTo = DateTime.UtcNow;
            }
            else if (request.DateFrom.HasValue && request.DateTo.HasValue)
            {
                if (request.DateTo.Equals(request.DateFrom) && request.DateTo?.TimeOfDay == TimeSpan.Zero)
                    request.DateTo = request.DateTo?.Date.AddDays(1).AddSeconds(-1);
            }

            PaginatedList<GetPostsDto> postDto = await _context.Posts
                    .AsNoTracking()
                    .Include(p => p.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.User)
                    .Where(p => (request.UserId == null || p.UserId == request.UserId)
                                && (request.DateFrom == null || p.CreatedTime >= request.DateFrom)
                                && (request.DateTo == null || p.CreatedTime <= request.DateTo))
                    .ProjectTo<GetPostsDto>(_mapper.ConfigurationProvider)
                    .ToPaginatedListAsync(request.PageIndex.Value, request.PageSize.Value, cancellationToken);

            if (postDto.Item.Any())
                return new GetPostsResponse(postDto, true, ResultCodes.Success, ResultMessage.GetPostsSuccess);
            else
                return new GetPostsResponse(postDto, true, ResultCodes.ListEmpty, ResultMessage.PostsListEmpty);
        }
    }
}
