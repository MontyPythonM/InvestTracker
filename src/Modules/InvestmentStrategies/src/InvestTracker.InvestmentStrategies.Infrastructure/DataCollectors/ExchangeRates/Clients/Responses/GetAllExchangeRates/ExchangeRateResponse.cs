namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetAllExchangeRates;

public sealed class ExchangeRateResponse
{
    public string Table { get; set; } = string.Empty;
    public string No { get; set; } = string.Empty;
    public DateOnly EffectiveDate { get; set; }
    public List<ExchangeRateRowResponse> Rates { get; set; } = new();
}