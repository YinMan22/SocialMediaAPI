using AutoMapper;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Paginations;
using SocialMedia.Domain.Entities;
using static SocialMedia.Application.Entities.Queries.Posts.GetLikePosts.GetLikePostsResponse;
using static SocialMedia.Application.Entities.Queries.Posts.GetPosts.GetPostsResponse;

namespace SocialMedia.Application.Entities.Queries.Posts.GetLikePosts
{
    public class GetLikePostsResponse : PaginatedResponse<LikePostDto>, IResponse
    {
        public GetLikePostsResponse() : base(null, false, "", "")
        {
        }

        public GetLikePostsResponse(PaginatedList<LikePostDto> paginatedItemList, bool isSuccess, string resultCode, string message)
            : base(paginatedItemList, isSuccess, resultCode, message)
        {
        }

        public class LikePostDto : IMapFrom<LikePost>
        {
            public PostDto Post { get; set; }
            public DateTime LikeAt { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<LikePost, LikePostDto>()
                    .ForMember(d => d.LikeAt, opt => opt.MapFrom(s => s.CreatedTime));
            }
        }

        public class PostDto : IMapFrom<Post>
        {
            public int Id { get; set; }
            public UserDto Owner { get; set; }
            public DateTime CreatedTime { get; set; }
            public string Content { get; set; }
            public byte[]? Image { get; set; }
            public int Likes { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Post, PostDto>()
                    .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.User));
            }
        }

    }
}
