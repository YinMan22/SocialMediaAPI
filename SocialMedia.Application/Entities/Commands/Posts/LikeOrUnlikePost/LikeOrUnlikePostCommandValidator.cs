using FluentValidation;
using SocialMedia.Application.Common.Constants;

namespace SocialMedia.Application.Entities.Commands.Posts.LikeOrUnlikePost
{
    public class LikeOrUnlikePostCommandValidator : AbstractValidator<LikeOrUnlikePostCommand>
    {
        public LikeOrUnlikePostCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(c => c.PostId)
                .GreaterThan(0)
                    .WithErrorCode(ResultCodes.IdValueInvalid)
                    .WithMessage("{PropertyName} must be non-zero and positve figure.");
        }
    }
}
