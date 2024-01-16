using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCoiVolumeChartHandler : IQueryHandler<GetCoiVolumeChart, VolumeChart>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IResourceAccessor _resourceAccessor;

    public GetCoiVolumeChartHandler(InvestmentStrategiesDbContext context, IResourceAccessor resourceAccessor)
    {
        _context = context;
        _resourceAccessor = resourceAccessor;
    }

    public async Task<VolumeChart> HandleAsync(GetCoiVolumeChart query, CancellationToken token = default)
    {
        await _resourceAccessor.CheckAsync(query.PortfolioId, token);

        var coi = await _context.CoiTreasuryBonds
            .AsNoTracking()
            .Include(asset => asset.Transactions)
            .SingleOrDefaultAsync(asset => asset.Id == query.FinancialAssetId && asset.PortfolioId == query.PortfolioId, token);

        if (coi is null)
        {
            throw new FinancialAssetNotFoundException(query.FinancialAssetId);
        }
        
        var volumes = coi.Transactions.Select(transaction => new ChartValue<DateOnly, int>
        {
            X = transaction.TransactionDate.ToDateOnly(),
            Y = transaction.Volume
        });

        return new VolumeChart(volumes, coi.Symbol, coi.Currency);
    }
}