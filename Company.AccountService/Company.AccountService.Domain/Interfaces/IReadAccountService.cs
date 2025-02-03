using Company.AccountService.Domain.Models;

namespace Company.AccountService.Domain.Interfaces
{
    public interface IReadAccountService
    {
        Task<Account?> GetAccountAsync(Guid accountId);
    }
}