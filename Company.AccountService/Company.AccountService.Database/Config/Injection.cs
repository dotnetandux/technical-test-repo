using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.AccountService.Database.Config
{
    public static class Injection
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AccountDbContext>(options =>
                options.UseInMemoryDatabase("AccountsDatabase"));
        }
    }
}
