using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Advisors.UpdateAdvisor;
using InvestTracker.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class AdvisorsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AdvisorsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPatch]
    [SwaggerOperation("Advisor can update a few data about himself")]
    public async Task<ActionResult> UpdateAdvisorDetails([FromBody] UpdateAdvisor command, CancellationToken token)
    {
        var currentUserId = Guid.NewGuid(); // TODO get user id from request
        await _commandDispatcher.SendAsync(command with { Id = currentUserId }, token);
        return Ok();
    }
}