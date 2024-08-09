using FluentValidation;
using SocialMedia.Application.Common.Constants;

namespace SocialMedia.Application.Entities.Commands.Posts.CommentPost
{
    public class CommentPostCommandValidator: AbstractValidator<CommentPostCommand>
    {
        public CommentPostCommandValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(c => c.PostId)
                .GreaterThan(0)
                    .WithErrorCode(ResultCodes.IdValueInvalid)
                    .WithMessage("{PropertyName} must be non-zero and positve figure.");

            RuleFor(c => c.Comment)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(1000)
                    .WithErrorCode(ResultCodes.ValueLengthInvalid)
                    .WithMessage("{PropertyName} cannot be more than {MaxLength} characters. Entered length: {TotalLength}.");
        }
    }
}
