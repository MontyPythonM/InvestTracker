namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;

public class VolumeChart
{
    public IEnumerable<ChartValue<DateOnly, int>> Volumes { get; set; }
    public string Symbol { get; set; }
    public string InCurrency { get; set; }
    
    public VolumeChart(IEnumerable<ChartValue<DateOnly, int>> volumes, string symbol, string currency)
    {
        Volumes = volumes;
        Symbol = symbol;
        InCurrency = currency;
    }
}