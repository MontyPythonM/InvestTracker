using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class InvestmentStrategiesController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IRequestContext _requestContext;

    public InvestmentStrategiesController(ICommandDispatcher commandDispatcher, IRequestContext requestContext)
    {
        _commandDispatcher = commandDispatcher;
        _requestContext = requestContext;
    }
    
    [HttpPost]
    [HasPermission(InvestmentStrategiesPermission.CreateInvestmentStrategy)]
    [SwaggerOperation("Create new investment strategy")]
    public async Task<ActionResult> CreateInvestmentStrategy(CreateInvestmentStrategy command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}