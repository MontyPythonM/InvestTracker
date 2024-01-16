using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers.FinancialAssets;

internal class CoiTreasuryBondsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CoiTreasuryBondsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{portfolioId:guid}/edo/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetCoiBond)]
    [SwaggerOperation("Return COI Treasury Bond financial asset details")]
    public async Task<ActionResult<CoiBondDto>> GetCoiBond(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCoiBond(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpGet("{portfolioId:guid}/edo/{assetId:guid}/volume-chart")]
    [HasPermission(InvestmentStrategiesPermission.GetCoiVolumeChart)]
    [SwaggerOperation("Return COI treasury bond volume chart")]
    public async Task<ActionResult<VolumeChart>> GetCoiVolumeChart(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCoiVolumeChart(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpGet("{portfolioId:guid}/edo/{assetId:guid}/amount-chart")]
    [HasPermission(InvestmentStrategiesPermission.GetCoiAmountChart)]
    [SwaggerOperation("Return COI treasury bond amount chart")]
    public async Task<ActionResult<AmountChart>> GetCoiAmountChart(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetCoiAmountChart(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
}