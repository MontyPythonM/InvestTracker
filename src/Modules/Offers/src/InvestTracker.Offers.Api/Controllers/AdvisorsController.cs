using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core.Features.Advisors.UpdateAdvisor;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class AdvisorsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IRequestContext _requestContext;

    public AdvisorsController(ICommandDispatcher commandDispatcher, IRequestContext requestContext)
    {
        _commandDispatcher = commandDispatcher;
        _requestContext = requestContext;
    }
    
    [HttpPatch]
    [HasPermission(OffersPermission.UpdateAdvisorDetails)]
    [SwaggerOperation("Advisor can update a few data about himself")]
    public async Task<ActionResult> UpdateAdvisorDetails([FromBody] UpdateAdvisor command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command with { Id = _requestContext.Identity.UserId }, token);
        return Ok();
    }
}