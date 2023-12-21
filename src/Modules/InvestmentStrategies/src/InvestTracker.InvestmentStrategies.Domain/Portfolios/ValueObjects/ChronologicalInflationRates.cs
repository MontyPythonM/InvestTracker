using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public sealed record ChronologicalInflationRates
{
    public IOrderedEnumerable<InflationRate> Values { get; }

    public ChronologicalInflationRates(IEnumerable<InflationRate> inflationRates)
    {
        var rates = inflationRates.ToList();
        
        if (!IsInflationRatesHaveSameCurrency(rates))
        {
            throw new InvalidInflationRatesCurrencyException();
        }

        if (!IsInflationRateHaveConsecutiveYears(rates))
        {
            throw new InflationRatesYearsInconsistencyException();
        }
        
        Values = rates
            .OrderBy(rate => rate.MonthlyDate.Year)
            .ThenBy(rate => rate.MonthlyDate.Month);
    }

    private static bool IsInflationRateHaveConsecutiveYears(IEnumerable<InflationRate> inflationRates)
    {
        var sortedDates = inflationRates
            .OrderBy(rate => rate.MonthlyDate.Year)
            .ThenBy(rate => rate.MonthlyDate.Month)
            .ToList();

        for (var i = 1; i < sortedDates.Count; i++)
        {
            var currentRate = sortedDates[i];
            var previousRate = sortedDates[i - 1];
            
            if (IsDatesSorted(currentRate, previousRate))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsDatesSorted(InflationRate currentRate, InflationRate previousRate)
        => currentRate.MonthlyDate.Year < previousRate.MonthlyDate.Year || 
           (currentRate.MonthlyDate.Year == previousRate.MonthlyDate.Year && currentRate.MonthlyDate.Month <= previousRate.MonthlyDate.Month);

    private static bool IsInflationRatesHaveSameCurrency(IReadOnlyCollection<InflationRate> inflationRates)
        => inflationRates.All(rate => rate.Currency == inflationRates.First().Currency);
}