using FluentValidation;
using SocialMedia.Application.Common.Constants;

namespace SocialMedia.Application.Entities.Commands.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Username)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(50)
                    .WithErrorCode(ResultCodes.ValueLengthInvalid)
                    .WithMessage("{PropertyName} cannot be more than {MaxLength} characters. Entered length: {TotalLength}.")
                .Matches(@"^[0-9A-Za-z]+$")
                    .WithErrorCode(ResultCodes.ValueNotMeetRequirement)
                    .WithMessage("{PropertyName} must contain only alphabets and numbers.");

            RuleFor(x => x.Password)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.")
                .MinimumLength(8)
                    .WithErrorCode(ResultCodes.ValueLengthInvalid)
                    .WithMessage("{PropertyName} must be more than {MinLength} characters. Entered length: {TotalLength}.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
                    .WithErrorCode(ResultCodes.ValueNotMeetRequirement)
                    .WithMessage("{PropertyName} must contain at least one uppercase letter, one lowercase letter and one number.");

            RuleFor(x => x.FullName)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(100)
                    .WithErrorCode(ResultCodes.ValueLengthInvalid)
                    .WithMessage("{PropertyName} cannot be more than {MaxLength} characters. Entered length: {TotalLength}.")
                .Matches(@"^[a-zA-Z ]+$")
                    .WithErrorCode(ResultCodes.ValueNotMeetRequirement)
                    .WithMessage("{PropertyName} must contain only alphabets and spaces.");
        }    
    }
}
