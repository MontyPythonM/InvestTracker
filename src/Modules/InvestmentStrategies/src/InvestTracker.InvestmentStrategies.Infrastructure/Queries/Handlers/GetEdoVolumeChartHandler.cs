﻿using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Shared;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetEdoVolumeChartHandler : IQueryHandler<GetEdoVolumeChart, VolumeChart>
{
    private readonly IResourceAccessor _resourceAccessor;
    private readonly InvestmentStrategiesDbContext _context;

    public GetEdoVolumeChartHandler(IResourceAccessor resourceAccessor, InvestmentStrategiesDbContext context)
    {
        _resourceAccessor = resourceAccessor;
        _context = context;
    }

    public async Task<VolumeChart> HandleAsync(GetEdoVolumeChart query, CancellationToken token = default)
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
            Y = edo.CalculateVolume(transaction.Amount)
        });

        return new VolumeChart(volumes, edo.Symbol, edo.Currency);
    }
}