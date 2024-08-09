using FluentValidation.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> validationContext = new(request);

                IEnumerable<ValidationResult> _validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext)));

                ValidationFailure? failure = _validationResults
                    .SelectMany(r => r.Errors)
                    .Where(error => error is not null)
                    .FirstOrDefault();

                if (failure is not null)
                {
                    _logger.LogError("Validation failed for {requestType} | Error: {errorMessage}", typeof(TRequest), failure.ErrorMessage);
                    throw new FluentValidationException(failure.ErrorCode, failure.ErrorMessage);
                }
            }

            return await next();
        }
    }
}
