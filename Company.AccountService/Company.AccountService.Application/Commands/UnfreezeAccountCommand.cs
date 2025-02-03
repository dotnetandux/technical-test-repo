using MediatR;

namespace Company.AccountService.Domain.Commands
{
    public record UnfreezeAccountCommand(Guid AccountId) : IRequest<bool>;
}
