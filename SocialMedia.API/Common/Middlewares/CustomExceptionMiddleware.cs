using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Model;
using System.Net.Mime;

namespace SocialMedia.API.Common.Middlewares
{
    internal sealed class CustomExceptionMiddleware
    {
    private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Response response = null;
            string requestPath = context.Request.Path.HasValue ? context.Request.Path.Value : "/";

            switch (ex)
            {
                case FluentValidationException fluentValidationException:
                    _logger.LogError("Fluent validation exception for RequestPath={RequestPath} | ResultCode={ResultCode} | ResultMessage={ResultMessage}", requestPath, fluentValidationException.ResultCode, fluentValidationException.Message);
                    response = new Response(false, fluentValidationException.ResultCode, fluentValidationException.Message);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case BadRequestException badRequestException:
                    _logger.LogError("Bad request exception for RequestPath={RequestPath} | ResultCode={ResultCode} | ResultMessage={ResultMessage}", requestPath, badRequestException.ResultCode, badRequestException.Message);
                    response = new Response(false, badRequestException.ResultCode, badRequestException.Message);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                default:
                    _logger.LogError("Unhandled exception for RequestPath={RequestPath} | Exception={Exception}", requestPath, ex);
                    response = new Response(false, ResultCodes.UncaughtError, ResultMessage.UncaughtError);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            if (response is not null)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
