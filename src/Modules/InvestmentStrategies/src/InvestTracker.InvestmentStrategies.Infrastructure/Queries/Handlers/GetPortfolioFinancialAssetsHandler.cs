using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries;
using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries.Dto;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetPortfolioFinancialAssetsHandler : IQueryHandler<GetPortfolioFinancialAssets, IEnumerable<FinancialAssetDto>>
{
    private readonly InvestmentStrategiesDbContext _context;

    public GetPortfolioFinancialAssetsHandler(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FinancialAssetDto>> HandleAsync(GetPortfolioFinancialAssets query, 
        CancellationToken token = default)
    {
        return await _context.FinancialAssets
            .Where(asset => asset.PortfolioId.Value == query.PortfolioId)
            .Select(asset => new FinancialAssetDto
            {
                Id = asset.Id,
                Name = asset.GetAssetName(),
                Currency = asset.Currency
            })
            .ToListAsync(token);
    }
}