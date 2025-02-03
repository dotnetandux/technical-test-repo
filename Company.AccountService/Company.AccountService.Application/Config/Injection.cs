using Company.AccountService.Application.Services;
using Company.AccountService.Database.Config;
using Company.AccountService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Company.AccountService.Application.Config
{
    public static class Injection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReadAccountService, ReadAccountService>();

            services.AddDatabase();
        }
    }
}
