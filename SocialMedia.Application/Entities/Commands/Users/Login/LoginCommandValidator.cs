using FluentValidation;
using SocialMedia.Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Entities.Commands.Users.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Username)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.");

            RuleFor(x => x.Password)
                .NotNull()
                    .WithErrorCode(ResultCodes.ValueNull)
                    .WithMessage("{PropertyName} cannot be null.")
                .NotEmpty()
                    .WithErrorCode(ResultCodes.ValueEmptyOrWhitespaces)
                    .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
