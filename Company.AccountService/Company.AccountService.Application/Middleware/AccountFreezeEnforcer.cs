using System.Text.Json;
using Company.AccountService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Company.AccountService.Application.Middleware
{
    public class AccountFreezeEnforcer(RequestDelegate _next,
        ILogger<AccountFreezeEnforcer> _logger,
        IReadAccountService _readAccountService)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var accountId = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(accountId))
            {
                _logger.LogWarning("Account id was not found in the request.");

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync("id is required.");
                
                return;
            }

            var account = await _readAccountService.GetAccountAsync(new Guid(accountId));

            if (account is null || account.IsFrozen is false)
            {
                await _next(httpContext); // continue
                return;
            }

            _logger.LogWarning("Request for Account {accountId} was blocked. Account is frozen.", accountId);

            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            httpContext.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(new
            {
                Message = "This account is currently frozen."
            });

            await httpContext.Response.WriteAsync(jsonResponse);

            return;
        }
    }
}
