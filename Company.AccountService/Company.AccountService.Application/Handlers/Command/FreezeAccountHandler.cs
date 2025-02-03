using Company.AccountService.Domain.Commands;
using Company.AccountService.Database.Config;
using MediatR;

namespace Company.AccountService.Application.Handlers.Command
{
    public class FreezeAccountHandler(AccountDbContext _dbContext)
        : IRequestHandler<FreezeAccountCommand, bool>
    {
        public async Task<bool> Handle(FreezeAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts.FindAsync(request.AccountId);

            if (account == null) return false;

            account.IsFrozen = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
