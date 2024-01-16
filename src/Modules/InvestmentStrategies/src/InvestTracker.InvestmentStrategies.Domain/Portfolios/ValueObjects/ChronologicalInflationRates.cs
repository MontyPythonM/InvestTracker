using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public sealed record ChronologicalInflationRates
{
    public IEnumerable<InflationRate> InflationRates { get; }

    public ChronologicalInflationRates(IEnumerable<InflationRate> inflationRates)
    {
        var orderedRates = inflationRates
            .OrderBy(rate => rate.MonthlyDate.Year)
            .ThenBy(rate => rate.MonthlyDate.Month)
            .ToList();
        
        if (!IsInflationRatesHaveSameCurrency(orderedRates))
        {
            throw new InvalidInflationRatesCurrencyException();
        }

        if (!IsInflationRateHaveConsecutiveYears(orderedRates))
        {
            throw new InflationRatesYearsInconsistencyException();
        }

        InflationRates = orderedRates;
    }

    private static bool IsInflationRateHaveConsecutiveYears(IReadOnlyList<InflationRate> inflationRates)
    {
        for (var i = 1; i < inflationRates.Count; i++)
        {
            var currentRate = inflationRates[i];
            var previousRate = inflationRates[i - 1];
            
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