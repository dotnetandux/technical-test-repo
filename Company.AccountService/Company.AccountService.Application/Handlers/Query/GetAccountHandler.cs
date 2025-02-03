using Company.AccountService.Database.Config;
using Company.AccountService.Domain.Models;
using Company.AccountService.Domain.Queries;
using MediatR;

namespace Company.AccountService.Application.Handlers.Query
{
    public class GetAccountHandler(AccountDbContext _dbContext)
        : IRequestHandler<GetAccountQuery, Account?>
    {
        public async Task<Account?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
            => await _dbContext.Accounts.FindAsync(request.AccountId);
    }
}
