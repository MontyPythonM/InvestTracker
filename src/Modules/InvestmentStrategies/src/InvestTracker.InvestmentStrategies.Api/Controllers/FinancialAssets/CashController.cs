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

namespace InvestTracker.InvestmentStrategies.Api.Controllers.FinancialAssets;

internal sealed class CashController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CashController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{portfolioId:guid}/cash/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetCash)]
    [SwaggerOperation("Return cash financial asset details")]
    public async Task<ActionResult<CashDto>> GetCash(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCash(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }

    [HttpGet("{portfolioId:guid}/cash/{assetId:guid}/chart")]
    [HasPermission(InvestmentStrategiesPermission.GetCashChart)]
    [SwaggerOperation("Return cash chart with optional currency conversion")]
    public async Task<ActionResult<CashChart>> GetCashChart([FromQuery]GetCashChartDto dto, 
        Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCashChart(assetId, portfolioId, dto.DisplayInCurrency, new DateRange(dto.DateFrom, dto.DateTo));
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpPost("{portfolioId:guid}/cash/{assetId:guid}/add")]
    [HasPermission(InvestmentStrategiesPermission.AddCashTransaction)]    
    [SwaggerOperation("Add incoming cash transaction to exising cash financial asset")]
    public async Task<ActionResult> AddCashTransaction(CashTransactionDto dto, Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var command = new AddCashTransaction(assetId, portfolioId, dto.Amount, dto.TransactionDate, dto.Note);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpPost("{portfolioId:guid}/cash/{assetId:guid}/deduct")]
    [HasPermission(InvestmentStrategiesPermission.DeductCashTransaction)]    
    [SwaggerOperation("Add outgoing cash transaction to exising cash financial asset")]
    public async Task<ActionResult> DeductCashTransaction(CashTransactionDto dto, Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var command = new DeductCashTransaction(assetId, portfolioId, dto.Amount, dto.TransactionDate, dto.Note);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpDelete("{portfolioId:guid}/cash/{assetId:guid}/transaction/{transactionId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.RemoveCashTransaction)]    
    [SwaggerOperation("Remove transaction from cash financial asset")]
    public async Task<ActionResult> RemoveCashTransaction(Guid portfolioId, Guid assetId, Guid transactionId, CancellationToken token)
    {
        var command = new RemoveCashTransaction(portfolioId, assetId, transactionId);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}