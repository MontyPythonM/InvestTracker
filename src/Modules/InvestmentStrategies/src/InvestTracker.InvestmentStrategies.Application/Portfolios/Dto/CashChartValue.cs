using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class CashChartValue
{
    public DateOnly Date { get; set; }
    public string InCurrency { get; set; }
    public decimal Amount { get; set; }

    public CashChartValue()
    {
    }

    public CashChartValue(DateOnly date, Currency inCurrency, Amount amount)
    {
        Date = date;
        InCurrency = inCurrency;
        Amount = amount;
    }
}