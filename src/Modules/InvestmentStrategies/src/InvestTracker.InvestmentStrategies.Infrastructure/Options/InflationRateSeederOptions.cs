namespace InvestTracker.InvestmentStrategies.Infrastructure.Options;

public class InflationRateSeederOptions
{
    public bool Enabled { get; set; } = true;
    public string Path { get; set; } = string.Empty;
    public bool IgnoreErrors { get; set; } = false;
    public string RowTypeIdentifier { get; set; } = "Analogiczny miesi�c poprzedniego roku = 100";
    public int RowTypeIdentifierColumnIndex { get; set; } = 2;
    public int ValueColumnIndex { get; set; } = 5;
    public int YearColumnIndex { get; set; } = 3;
    public int MonthColumnIndex { get; set; } = 4;
    public string SelectorName { get; set; } = string.Empty;
}