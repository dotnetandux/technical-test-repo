using Company.AccountService.Database.Config;
using Company.AccountService.Domain.Interfaces;
using Company.AccountService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.AccountService.Application.Services
{
    public class ReadAccountService(AccountDbContext _dbContext) : IReadAccountService
    {
        public async Task<Account?> GetAccountAsync(Guid accountId)
        {
            return await _dbContext.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
        }
    }
}
