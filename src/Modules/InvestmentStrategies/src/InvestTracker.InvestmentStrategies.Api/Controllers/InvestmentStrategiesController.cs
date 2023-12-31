﻿using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Dto;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Queries;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class InvestmentStrategiesController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public InvestmentStrategiesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet]
    [HasPermission(InvestmentStrategiesPermission.GetInvestmentStrategies)]
    [SwaggerOperation("Returns the current user's investment strategies")]
    public async Task<ActionResult<IEnumerable<InvestmentStrategiesDto>>> GetInvestmentStrategies(CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetInvestmentStrategies(), token));
    
    [HttpGet("{id:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetInvestmentStrategyDetails)]
    [SwaggerOperation("Returns investment strategy with details about portfolios")]
    public async Task<ActionResult<InvestmentStrategyDetailsDto>> GetInvestmentStrategyDetails(Guid id, CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetInvestmentStrategyDetails(id), token));
    
    [HttpPost]
    [HasPermission(InvestmentStrategiesPermission.CreateInvestmentStrategy)]
    [SwaggerOperation("Create new investment strategy")]
    public async Task<ActionResult> CreateInvestmentStrategy(CreateInvestmentStrategy command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpPost("{id:guid}/portfolios")]
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