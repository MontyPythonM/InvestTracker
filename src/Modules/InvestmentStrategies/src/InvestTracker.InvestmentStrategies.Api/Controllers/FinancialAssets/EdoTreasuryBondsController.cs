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

internal sealed class EdoTreasuryBondsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public EdoTreasuryBondsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{portfolioId:guid}/coi/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetEdoBond)]
    [SwaggerOperation("Return EDO Treasury Bond financial asset details")]
    public async Task<ActionResult<EdoBondDto>> GetEdoBond(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetEdoBond(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpGet("{portfolioId:guid}/coi/{assetId:guid}/volume-chart")]
    [HasPermission(InvestmentStrategiesPermission.GetEdoVolumeChart)]
    [SwaggerOperation("Return EDO treasury bond volume chart")]
    public async Task<ActionResult<VolumeChart>> GetEdoVolumeChart(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetEdoVolumeChart(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
    
    [HttpGet("{portfolioId:guid}/coi/{assetId:guid}/amount-chart")]
    [HasPermission(InvestmentStrategiesPermission.GetEdoAmountChart)]
    [SwaggerOperation("Return EDO treasury bond amount chart")]
    public async Task<ActionResult<AmountChart>> GetEdoAmountChart(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetEdoAmountChart(assetId, portfolioId);
        return Ok(await _queryDispatcher.QueryAsync(query, token));
    }
}