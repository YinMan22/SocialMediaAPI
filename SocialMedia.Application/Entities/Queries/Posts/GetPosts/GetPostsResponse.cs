using AutoMapper;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Paginations;
using SocialMedia.Domain.Entities;
using static SocialMedia.Application.Entities.Queries.Posts.GetPosts.GetPostsResponse;

namespace SocialMedia.Application.Entities.Queries.Posts.GetPosts
{
    public class GetPostsResponse : PaginatedResponse<GetPostsDto>, IResponse
    {
        public GetPostsResponse() : base(null, false, "", "")
        {
        }

        public GetPostsResponse(PaginatedList<GetPostsDto> paginatedItemList, bool isSuccess, string resultCode, string message)
            : base(paginatedItemList, isSuccess, resultCode, message)
        {
        }

        public class GetPostsDto : IMapFrom<Post>
        {
            public int PostId { get; set; }
            public UserDto Owner { get; set; }
            public DateTime CreatedTime { get; set; }
            public string Content { get; set; }
            public byte[]? Image { get; set; }
            public int Likes { get; set; }
            public List<CommentDto> Comments { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Post, GetPostsDto>()
                    .ForMember(d => d.PostId, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.User));
            }
        }
        public class UserDto : IMapFrom<User>
        {
            public int UserId { get; set; }
            public string FullName { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<User, UserDto>()
                    .ForMember(d => d.UserId, otp => otp.MapFrom(s => s.Id));
            }
        }

        public class CommentDto : IMapFrom<Comment>
        {
            public UserDto Commentor { get; set; }
            public string Comment { get; set; }
            public DateTime CreatedTime { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Comment, CommentDto>()
                    .ForMember(d => d.Comment, otp => otp.MapFrom(s => s.Text))
                    .ForMember(d => d.Commentor, opt => opt.MapFrom(s => s.User));
            }
        }
    }
}
