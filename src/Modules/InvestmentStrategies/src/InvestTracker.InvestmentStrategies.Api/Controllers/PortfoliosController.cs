using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Dto;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Commands;
using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries;
using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries.Dto;
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
    
    [HttpPost("{portfolioId:guid}/cash")]
    [HasPermission(InvestmentStrategiesPermission.CreateCashAsset)]    
    [SwaggerOperation("Create new cash financial asset type in portfolio and optionally set initial amount")]
    public async Task<ActionResult> CreateCashAsset(AddCashAssetDto dto, Guid portfolioId, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new AddCashAsset(portfolioId, dto.Currency, dto.Note, dto.InitialAmount), token);
        return Ok();
    }
    
    [HttpPost("{portfolioId:guid}/edo")]
    [HasPermission(InvestmentStrategiesPermission.CreateEdoAsset)]
    [SwaggerOperation("Create new EDO treasury bond financial asset type in portfolio")]
    public async Task<ActionResult> CreateEdoAsset(AddEdoBondAssetDto dto, Guid portfolioId, CancellationToken token)
    {
        var command = new AddEdoBondAsset(portfolioId, dto.Volume, dto.PurchaseDate, dto.FirstYearInterestRate, dto.Margin, dto.Note);
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpGet("{portfolioId:guid}/financial-assets")]
    [HasPermission(InvestmentStrategiesPermission.GetPortfolioFinancialAssets)]
    [SwaggerOperation("Get list of financial assets for selected portfolio")]
    public async Task<ActionResult<IEnumerable<FinancialAssetDto>>> GetPortfolioFinancialAsset(Guid portfolioId, CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetPortfolioFinancialAssets(portfolioId), token));
}