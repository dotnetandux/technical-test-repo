using Company.AccountService.Domain.Commands;
using Company.AccountService.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.AccountService.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController(IMediator _mediator)
    {
        [HttpPost]
        public async Task<IActionResult> CreateAccount(
            [FromBody] CreateAccountCommand command)
        {
            var accountId = await _mediator.Send(command);

            return new CreatedAtActionResult(nameof(GetAccount), 
                null,
                new { id = accountId }, 
                accountId);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAccount(
            [FromRoute] Guid id)
        {
            var account = await _mediator.Send(new GetAccountQuery(id));

            return new OkObjectResult(account);
        }

        [HttpPatch("{id:Guid}/disable")]
        public async Task<IActionResult> FreezeAccount(
            [FromRoute] Guid id)
        {
            var success = await _mediator.Send(new FreezeAccountCommand(id));

            return success ? new OkResult() : new NotFoundResult();
        }

        [HttpPatch("{id:Guid}/enable")]    
        public async Task<IActionResult> UnfreezeAccount(
            [FromRoute] Guid id)
        {
            var success = await _mediator.Send(new UnfreezeAccountCommand(id));

            return success ? new OkResult() : new NotFoundResult();
        }
    }
}
