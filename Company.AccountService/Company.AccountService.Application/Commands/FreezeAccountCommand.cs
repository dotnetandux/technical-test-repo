using MediatR;

namespace Company.AccountService.Domain.Commands
{
    public record FreezeAccountCommand(Guid AccountId) : IRequest<bool>;
}
