using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetEdoAmountChartHandler : IQueryHandler<GetEdoAmountChart, AmountChart>
{
    private readonly IResourceAccessor _resourceAccessor;
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IInflationRateRepository _inflationRateRepository;

    public GetEdoAmountChartHandler(IResourceAccessor resourceAccessor, InvestmentStrategiesDbContext context, 
        IInflationRateRepository inflationRateRepository)
    {
        _resourceAccessor = resourceAccessor;
        _context = context;
        _inflationRateRepository = inflationRateRepository;
    }

    public async Task<AmountChart> HandleAsync(GetEdoAmountChart query, CancellationToken token = default)
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

        var inflationRates = await _inflationRateRepository
            .GetChronologicalRatesAsync(new DateRange(edo.PurchaseDate, edo.GetRedemptionDate()), token);

        var amounts = new List<ChartValue<DateOnly, decimal>>();
        var calculationDates = new List<DateOnly>();
        
        calculationDates.AddRange(edo.Transactions.Select(transaction => transaction.TransactionDate.ToDateOnly()));
        calculationDates.AddRange(edo.GetInvestmentPeriods().Select(dateRange => dateRange.To));
        
        foreach (var calculationDate in calculationDates.Order())
        {
            var amount = edo.GetAmount(inflationRates, calculationDate);
            amounts.Add(new ChartValue<DateOnly, decimal>{ X = calculationDate, Y = amount });
        }
        
        return new AmountChart(amounts, edo.Symbol, edo.Currency);
    }
}