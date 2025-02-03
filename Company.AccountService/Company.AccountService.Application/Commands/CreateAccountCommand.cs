using Company.AccountService.Domain.Models.Enums;
using MediatR;

namespace Company.AccountService.Domain.Commands
{
    public record CreateAccountCommand(string AccountNumber,
        string SortCode,
        string DisplayName,
        decimal StartingBalance,
        AccountTypes Type) : IRequest<Guid>;
}
