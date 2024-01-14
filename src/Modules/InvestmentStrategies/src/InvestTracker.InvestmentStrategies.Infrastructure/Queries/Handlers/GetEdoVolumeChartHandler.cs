using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetEdoVolumeChartHandler : IQueryHandler<GetEdoVolumeChart, EdoVolumeChart>
{
    private readonly IResourceAccessor _resourceAccessor;
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IChartService _chartService;

    public GetEdoVolumeChartHandler(IResourceAccessor resourceAccessor, InvestmentStrategiesDbContext context, IChartService chartService)
    {
        _resourceAccessor = resourceAccessor;
        _context = context;
        _chartService = chartService;
    }

    public async Task<EdoVolumeChart> HandleAsync(GetEdoVolumeChart query, CancellationToken token = default)
    {
        await _resourceAccessor.CheckAsync(query.PortfolioId, token);

        var edo = await _context.EdoTreasuryBonds
            .AsNoTracking()
            .Include(asset => asset.Transactions)
            .SingleOrDefaultAsync(asset => asset.Id == query.FinancialAssetId && asset.PortfolioId == query.PortfolioId, token);

        if (edo is null)
        {
            throw new FinancialAssetNotFoundException(query.FinancialAssetId);
        }
        
        var volumes = edo.Transactions.Select(transaction => new ChartValue<DateOnly, int>
        {
            X = transaction.TransactionDate.ToDateOnly(),
            Y = transaction.Volume
        });

        return new EdoVolumeChart(volumes, edo.Symbol, edo.Currency);
    }
}