using Company.AccountService.Domain.Models;
using MediatR;

namespace Company.AccountService.Domain.Queries
{
    public record GetAccountQuery(Guid AccountId) : IRequest<Account?>;
}
