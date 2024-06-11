using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DapperProj.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            ProblemDetails problemDetails;

            if (exception is ArgumentException)
            {
                problemDetails = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Bad Request",
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
            }
            else if (exception is InvalidOperationException){
                 problemDetails = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Bad Request",
                    Title = "InvalidOperationException",
                    Detail = exception.Message
                };
            }
            else
            {
                problemDetails = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "Server Error",
                    Detail = "Error"
                };
            }

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}