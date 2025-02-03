using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Company.AccountService.Api
{
    public class ExceptionHandler(ILogger<ExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if (httpContext.Response.StatusCode is StatusCodes.Status400BadRequest)
            {
                return false;
            }

            var endpoint = httpContext.Request.Path;

            _logger.LogError(exception, 
                "Error occured while processing request for {endpoint}", 
                endpoint);

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(new
            {
                Message = "An unexpected error occurred whilst processing your request."
            });

            await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

            return await Task.FromResult(true);
        }
    }
}

