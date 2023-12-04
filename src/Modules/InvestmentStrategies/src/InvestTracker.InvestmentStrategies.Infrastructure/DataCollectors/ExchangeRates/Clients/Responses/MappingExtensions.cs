using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetAllExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetConcreteCurrencyExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses;

internal static class MappingExtensions
{
    internal static IEnumerable<ExchangeRateEntity> MapToExchangeRateEntities(this List<ExchangeRateResponse> responses, DateTime now)
    {
        var exchangeRateEntities = new List<ExchangeRateEntity>();
        foreach (var response in responses)
        {
            foreach (var rate in response.Rates)
            {
                if (!Currencies.IsSupported(rate.Code))
                {
                    continue;
                }

                var exchangeRate = new ExchangeRateEntity(Guid.NewGuid(), rate.Code, Currencies.PLN,
                    response.EffectiveDate, now, rate.Mid, response.No);
                
                exchangeRateEntities.Add(exchangeRate);
            }
        }

        return exchangeRateEntities;
    }
    
    internal static IEnumerable<ExchangeRateEntity> MapToExchangeRateEntities(this ConcreteExchangeRatesResponse response, DateTime now)
    {
        var exchangeRateEntities = new List<ExchangeRateEntity>();

        foreach (var rate in response.Rates)
        {
            var exchangeRate = new ExchangeRateEntity(Guid.NewGuid(), response.Code, Currencies.PLN,
                rate.EffectiveDate, now, rate.Mid, rate.No);
                
            exchangeRateEntities.Add(exchangeRate);
        }
        
        return exchangeRateEntities;
    }
}