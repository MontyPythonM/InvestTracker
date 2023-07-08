using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Advisors.UpdateAdvisor;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class AdvisorsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;

    public AdvisorsController(ICommandDispatcher commandDispatcher, IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _context = context;
    }
    
    [HttpPatch]
    [SwaggerOperation("Advisor can update a few data about himself")]
    public async Task<ActionResult> UpdateAdvisorDetails([FromBody] UpdateAdvisor command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command with { Id = _context.Identity.UserId }, token);
        return Ok();
    }
}