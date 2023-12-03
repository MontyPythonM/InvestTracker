using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Dto;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Commands;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class FinancialAssetsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public FinancialAssetsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{portfolioId:guid}/cash/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetCashChart)]
    [SwaggerOperation("Return cash chart with optional currency conversion")]
    public async Task<ActionResult<IEnumerable<CashChartValue>>> GetCashChart([FromQuery]GetCashChartDto dto, 
        Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCashChart(assetId, portfolioId, dto.DisplayInCurrency, new DateRange(dto.DateFrom, dto.DateTo));
        return OkOrNotFound(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpPost("{portfolioId:guid}/cash/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.AddCashTransaction)]    
    [SwaggerOperation("Add cash transaction to exising cash financial asset")]
    public async Task<ActionResult> AddCash(AddCashTransactionDto dto, Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var command = new AddCashTransaction(assetId, portfolioId, dto.Amount, dto.TransactionDate, dto.Note);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

}