namespace InvestTracker.InvestmentStrategies.Infrastructure.Options;

public class InflationRateApiOptions
{
    public bool Enabled { get; set; } = true;
    public int MaxErrorsNumber { get; set; } = 0;
    public int UpdateMissingFromYear { get; set; } = 2010;
    public int DurationDays { get; set; } = 10;
    public int RecordsNumber { get; set; } = 2000;
    public int PositionId1 { get; set; } = 33617;
    public int PositionId2 { get; set; } = 6656078;
    public int PositionId3 { get; set; } = 6902025;
    public int ReferenceMeasure { get; set; } = 5;
    public string SelectorName { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 20;
}