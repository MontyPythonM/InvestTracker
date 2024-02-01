using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCoiAmountChartHandler : IQueryHandler<GetCoiAmountChart, AmountChart>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IResourceAccessor _resourceAccessor;
    private readonly IInflationRateRepository _inflationRateRepository;

    public GetCoiAmountChartHandler(InvestmentStrategiesDbContext context, IResourceAccessor resourceAccessor, 
        IInflationRateRepository inflationRateRepository)
    {
        _context = context;
        _resourceAccessor = resourceAccessor;
        _inflationRateRepository = inflationRateRepository;
    }

    public async Task<AmountChart> HandleAsync(GetCoiAmountChart query, CancellationToken token = default)
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

        var inflationRates = await _inflationRateRepository
            .GetChronologicalRatesAsync(new DateRange(coi.PurchaseDate, coi.GetRedemptionDate()), token);

        var calculationDates = new List<DateOnly>();
        calculationDates.AddRange(coi.Transactions.Select(transaction => transaction.TransactionDate.ToDateOnly()));
        calculationDates.AddRange(coi.GetInvestmentPeriods().Select(dateRange => dateRange.To));
        
        var amounts = calculationDates
            .Order()
            .Select(date => new ChartValue<DateOnly, decimal>
            {
                X = date, 
                Y = coi.GetAmount(inflationRates, date)
            });
        
        return new AmountChart(amounts, coi.Symbol, coi.Currency);    
    }
}