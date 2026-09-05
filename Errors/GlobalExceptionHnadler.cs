using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Errors
{
    public class GlobalExceptionHnadler(ILogger<GlobalExceptionHnadler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHnadler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,"Something went wrong {Message}",exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "ServerError",
                Type = "https://datatracker.ietf.org/doc/html/rfc9110"
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
