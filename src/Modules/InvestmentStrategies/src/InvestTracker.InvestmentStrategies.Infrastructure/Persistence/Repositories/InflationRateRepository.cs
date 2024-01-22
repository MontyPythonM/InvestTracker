using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class InflationRateRepository : IInflationRateRepository
{
    private readonly InvestmentStrategiesDbContext _context;
    
    public InflationRateRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InflationRate>> GetInflationRatesAsync(CancellationToken token = default)
    {
        return await _context.InflationRates
            .Select(rate => new InflationRate(rate.Value, rate.Currency, rate.MonthlyDate.Year, rate.MonthlyDate.Month))
            .ToListAsync(token);    
    }
    
    public async Task<IEnumerable<InflationRate>> GetInflationRatesAsync(DateRange dateRange, 
        CancellationToken token = default)
    {
        return await _context.InflationRates
            .Where(rate => rate.MonthlyDate.ToDateOnly() >= dateRange.From.AsFirstDayOfMonth() && 
                           rate.MonthlyDate.ToDateOnly() <= dateRange.To.AsFirstDayOfMonth())
            .Select(rate => new InflationRate(rate.Value, rate.Currency, rate.MonthlyDate.Year, rate.MonthlyDate.Month))
            .ToListAsync(token);
    }

    public async Task<ChronologicalInflationRates> GetChronologicalRatesAsync(DateRange dateRange, 
        CancellationToken token = default)
    {
        var rates = await _context.InflationRates
            .Where(rate => rate.MonthlyDate.ToDateOnly() >= dateRange.From.AsFirstDayOfMonth() && 
                           rate.MonthlyDate.ToDateOnly() <= dateRange.To.AsFirstDayOfMonth())
            .OrderBy(rate => rate.MonthlyDate.Year)
            .ThenBy(rate => rate.MonthlyDate.Month)
            .Select(rate => new InflationRate(rate.Value, rate.Currency, rate.MonthlyDate.Year, rate.MonthlyDate.Month))
            .ToListAsync(token);

        return new ChronologicalInflationRates(rates);
    }
}