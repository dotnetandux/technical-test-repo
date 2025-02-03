using System.Text.Json;
using Company.AccountService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Company.AccountService.Application.Middleware
{
    public class AccountFreezeEnforcer(RequestDelegate _next,
        ILogger<AccountFreezeEnforcer> _logger,
        IServiceScopeFactory _scopeFactory)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            using var scope = _scopeFactory.CreateScope();
            var readAccountService = 
                scope.ServiceProvider.GetRequiredService<IReadAccountService>();

            var routeData = RoutingHttpContextExtensions.GetRouteData(httpContext);

            var accountId = routeData?.Values["id"]?.ToString();

            if (string.IsNullOrWhiteSpace(accountId))
            {
                _logger.LogWarning("Account id was not found in the request.");

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync("id is required.");
                
                return;
            }

            var account = await readAccountService.GetAccountAsync(new Guid(accountId));

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
