using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class CashChart
{
    public IEnumerable<ChartValue<DateOnly, decimal>> Amounts { get; set; }
    public string InCurrency { get; set; }

    public CashChart(Currency inCurrency, IEnumerable<ChartValue<DateOnly, decimal>> amounts)
    {
        InCurrency = inCurrency;
        Amounts = amounts;
    }
}