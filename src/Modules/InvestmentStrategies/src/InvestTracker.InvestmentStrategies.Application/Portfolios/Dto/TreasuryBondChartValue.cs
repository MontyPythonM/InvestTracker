using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class TreasuryBondChartValue
{
    public DateOnly Date { get; set; }
    public string InCurrency { get; set; }
    public int Volume { get; set; }
    public decimal Amount { get; set; }

    public TreasuryBondChartValue()
    {
    }

    public TreasuryBondChartValue(DateOnly date, Currency inCurrency, Volume volume, Amount amount)
    {
        Date = date;
        InCurrency = inCurrency;
        Volume = volume;
        Amount = amount;
    }
}