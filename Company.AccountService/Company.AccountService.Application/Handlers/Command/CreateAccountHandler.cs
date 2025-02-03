using Company.AccountService.Database.Config;
using Company.AccountService.Domain.Commands;
using Company.AccountService.Domain.Models;
using Company.AccountService.Domain.Models.AccountTypes;
using Company.AccountService.Domain.Models.Enums;
using MediatR;

namespace Company.AccountService.Application.Handlers.Command
{
    public class CreateAccountHandler(AccountDbContext _dbContext)
        : IRequestHandler<CreateAccountCommand, Guid>
    {
        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account newAccount = request.Type switch
            {
                AccountTypes.Current => new CurrentAccount 
                { 
                    AccountNumber = request.AccountNumber, 
                    DisplayName = request.DisplayName, 
                    SortCode = request.SortCode
                },

                AccountTypes.Savings => new SavingsAccount 
                { 
                    AccountNumber = request.AccountNumber, 
                    DisplayName = request.DisplayName, 
                    SortCode = request.SortCode
                },

                _ => throw new ArgumentOutOfRangeException("Unknown account type.")
            };

            _dbContext.Accounts.Add(newAccount);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newAccount.Id;
        }
    }
}
