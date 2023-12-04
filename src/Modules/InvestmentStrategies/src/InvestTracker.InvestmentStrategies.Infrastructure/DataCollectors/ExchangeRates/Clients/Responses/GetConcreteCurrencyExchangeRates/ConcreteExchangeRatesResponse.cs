namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetConcreteCurrencyExchangeRates;

public class ConcreteExchangeRatesResponse
{
    public string Table { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public List<ConcreteExchangeRateRowResponse> Rates { get; set; } = new();
}