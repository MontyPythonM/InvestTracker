using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Dtos;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class InvestmentStrategiesController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public InvestmentStrategiesController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    [HasPermission(InvestmentStrategiesPermission.CreateInvestmentStrategy)]
    [SwaggerOperation("Create new investment strategy")]
    public async Task<ActionResult> CreateInvestmentStrategy(CreateInvestmentStrategy command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpPost("{id:guid}/add-portfolio")]
    [HasPermission(InvestmentStrategiesPermission.AddPortfolio)]
    [SwaggerOperation("Add new portfolio to investment strategy")]
    public async Task<ActionResult> AddPortfolio(AddPortfolioDto dto, Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new AddPortfolio(id, dto.Title, dto.Description, dto.Note), token);
        return Ok();
    }
    
    [HttpPost("{id:guid}/share")]
    [HasPermission(InvestmentStrategiesPermission.ShareInvestmentStrategy)]
    [SwaggerOperation("Share investment strategy with assigned collaborator")]
    public async Task<ActionResult> ShareInvestmentStrategy(ShareInvestmentStrategyDto dto, Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new ShareInvestmentStrategy(id, dto.ShareWith), token);
        return Ok();
    }
}