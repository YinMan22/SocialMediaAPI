using FluentValidation;
using SocialMedia.Application.Common.Constants;

namespace SocialMedia.Application.Entities.Queries.Posts.GetPosts
{
    public class GetPostsValidator : AbstractValidator<GetPostsQuery>
    {
        public GetPostsValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(c => c.UserId)
                .GreaterThan(0)
                    .WithErrorCode(ResultCodes.IdValueInvalid)
                    .WithMessage("{PropertyName} must be non-zero and positve figure.");
        }
    }
}
