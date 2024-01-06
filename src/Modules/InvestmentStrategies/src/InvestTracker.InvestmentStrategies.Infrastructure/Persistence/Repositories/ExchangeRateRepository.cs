using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly InvestmentStrategiesDbContext _context;
    
    public ExchangeRateRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ExchangeRate>> GetAsync(Currency fromCurrency, Currency toCurrency, DateRange dateRange, 
        CancellationToken token = default)
    {
        if (fromCurrency == toCurrency)
        {
            return dateRange
                .GetDates()
                .Select(date => new ExchangeRate(fromCurrency, toCurrency, date, 1))
                .ToList();
        }
        
        const string baseToCurrency = Currencies.PLN;
        var exchangeRateEntities = new List<ExchangeRate>();
        
        var baseQuery = _context.ExchangeRates
            .AsNoTracking()
            .Where(rate => rate.Date >= dateRange.From && rate.Date <= dateRange.To)
            .AsQueryable();
        
        if (fromCurrency != baseToCurrency && toCurrency == baseToCurrency)
        {
            var rates = await baseQuery
                .Where(rate => rate.From == fromCurrency && 
                               rate.To == baseToCurrency)
                .Select(rate => new ExchangeRate(rate.From, rate.To, rate.Date, rate.Value))
                .ToListAsync(token);
            
            exchangeRateEntities.AddRange(rates);
        }

        if (fromCurrency == baseToCurrency && toCurrency != baseToCurrency)
        {
            var rates = await baseQuery
                .Where(rate => rate.From == toCurrency && 
                               rate.To == baseToCurrency)
                .Select(rate => new ExchangeRate(rate.From, rate.To, rate.Date, 1 / rate.Value))
                .ToListAsync(token);
            
            exchangeRateEntities.AddRange(rates);
        }

        if (fromCurrency != baseToCurrency && toCurrency != baseToCurrency)
        {
            var rates = await baseQuery
                .Where(rate => (rate.From == fromCurrency && rate.To == baseToCurrency) || 
                               (rate.From == toCurrency && rate.To == baseToCurrency))
                .ToListAsync(token);

            foreach (var rate in rates.GroupBy(rate => rate.Date))
            {
                var exchangeFromValue = rate.SingleOrDefault(r => r.From == fromCurrency);
                var exchangeToValue = rate.SingleOrDefault(r => r.From == toCurrency);

                if (exchangeFromValue is null || exchangeToValue is null)
                {
                    continue;
                }
                
                var calculatedRate = new ExchangeRate(
                    exchangeFromValue.From, 
                    exchangeToValue.From, 
                    exchangeFromValue.Date, 
                    exchangeFromValue.Value / exchangeToValue.Value);
                
                exchangeRateEntities.Add(calculatedRate);
            }
        }

        return exchangeRateEntities;    
    }
}