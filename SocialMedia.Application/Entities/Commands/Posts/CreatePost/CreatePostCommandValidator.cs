using FluentValidation;
using SocialMedia.Application.Common.Constants;

namespace SocialMedia.Application.Entities.Commands.Posts.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator() 
        {
            RuleFor(c => c.Content)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(1000)
                    .WithErrorCode(ResultCodes.ValueLengthInvalid)
                    .WithMessage("{PropertyName} cannot be more than {MaxLength} characters. Entered length: {TotalLength}.");

            RuleFor(c => c.Image)
               .NotEmpty()
                   .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                   .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
