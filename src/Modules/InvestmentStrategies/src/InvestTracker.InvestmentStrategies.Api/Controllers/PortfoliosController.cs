﻿using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Dto;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Commands;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class PortfoliosController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public PortfoliosController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{portfolioId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetPortfolioDetails)]
    [SwaggerOperation("Returns portfolio details")]
    public async Task<ActionResult<PortfolioDetailsDto>> GetPortfolioDetails(Guid portfolioId, CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetPortfolio(portfolioId), token));
    
    [HttpPost("{portfolioId:guid}/cash")]
    [HasPermission(InvestmentStrategiesPermission.CreateCashAsset)]    
    [SwaggerOperation("Create new cash financial asset type in portfolio and optionally set initial amount")]
    public async Task<ActionResult> CreateCashAsset(CreateCashAssetDto dto, Guid portfolioId, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new CreateCashAsset(portfolioId, dto.Currency, dto.Note, dto.InitialAmount, dto.InitialDate), token);
        return Ok();
    }
    
    [HttpPost("{portfolioId:guid}/edo")]
    [HasPermission(InvestmentStrategiesPermission.CreateEdoAsset)]
    [SwaggerOperation("Create new EDO treasury bond financial asset type in portfolio")]
    public async Task<ActionResult> CreateEdoAsset(AddEdoBondAssetDto dto, Guid portfolioId, CancellationToken token)
    {
        var command = new CrateEdoBondAsset(portfolioId, dto.Volume, dto.PurchaseDate, dto.FirstYearInterestRate, dto.Margin, dto.Note);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}