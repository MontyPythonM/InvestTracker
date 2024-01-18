namespace InvestTracker.InvestmentStrategies.Infrastructure.Options;

public class ExchangeRateApiOptions
{
    public int DurationHours { get; set; } = 168;
    public bool Enabled { get; set; } = true;
    public uint GetAllDaysRequestLimit { get; set; } = 92;
    public uint GetDaysRequestLimit { get; set; } = 365;
    public int MaxErrorsNumber { get; set; } = 0;
    public DateOnly UpdateMissingFromDate { get; set; } = new(2012, 01, 01);
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 20;
}