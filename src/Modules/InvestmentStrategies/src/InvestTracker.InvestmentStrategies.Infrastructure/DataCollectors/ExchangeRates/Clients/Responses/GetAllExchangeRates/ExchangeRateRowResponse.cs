namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetAllExchangeRates;

public sealed class ExchangeRateRowResponse
{
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal Mid { get; set; }
}