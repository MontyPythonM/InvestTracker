namespace InvestTracker.InvestmentStrategies.Infrastructure.Options;

public class ExchangeRateOptions
{
    public int DurationHours { get; set; } = 168;
    public bool Enabled { get; set; } = true;
    public int GetAllDaysRequestLimit { get; set; } = 92;
    public int GetDaysRequestLimit { get; set; } = 365;
    public int MaxErrorsNumber { get; set; } = 0;
    public DateOnly UpdateMissingFromDate { get; set; } = new(2012, 01, 01);
}