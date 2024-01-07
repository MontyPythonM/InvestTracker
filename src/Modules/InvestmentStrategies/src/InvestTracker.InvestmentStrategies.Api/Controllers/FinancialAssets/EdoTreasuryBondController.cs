using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers.FinancialAssets;

internal sealed class EdoTreasuryBondController : FinancialAssetsController
{
    public EdoTreasuryBondController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) 
        : base(commandDispatcher, queryDispatcher)
    {
    }
    
    [HttpGet("{portfolioId:guid}/bonds/{assetId:guid}")]
    [HasPermission(InvestmentStrategiesPermission.GetEdoTreasuryBond)]
    [SwaggerOperation("Return EDO Treasury Bond financial asset details")]
    public async Task<ActionResult<EdoTreasuryBondDto>> GetEdoTreasuryBond(Guid portfolioId, Guid assetId, CancellationToken token)
    {
        var query = new GetEdoTreasuryBond(assetId, portfolioId);
        return Ok(await QueryDispatcher.QueryAsync(query, token));
    }
}