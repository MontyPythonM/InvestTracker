namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetConcreteCurrencyExchangeRates;

public sealed class ConcreteExchangeRateRowResponse 
{
    public string No { get; set; }
    public DateOnly EffectiveDate { get; set; }
    public decimal Mid { get; set; }
}