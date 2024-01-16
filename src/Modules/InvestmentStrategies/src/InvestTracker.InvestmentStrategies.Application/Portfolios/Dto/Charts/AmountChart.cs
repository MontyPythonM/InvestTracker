namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;

public class AmountChart
{
    public IEnumerable<ChartValue<DateOnly, decimal>> Amounts { get; set; }
    public string Symbol { get; set; }
    public string InCurrency { get; set; }

    public AmountChart(IEnumerable<ChartValue<DateOnly, decimal>> amounts, string symbol, string currency)
    {
        Amounts = amounts;
        Symbol = symbol;
        InCurrency = currency;
    }
}