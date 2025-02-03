using Company.AccountService.Domain.Commands;
using Company.AccountService.Database.Config;
using MediatR;

namespace Company.AccountService.Application.Handlers.Command
{
    public class UnfreezeAccountHandler(AccountDbContext _dbContext)
        : IRequestHandler<UnfreezeAccountCommand, bool>
    {
        public async Task<bool> Handle(UnfreezeAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts.FindAsync(request.AccountId);

            if (account == null) return false;

            account.IsFrozen = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
