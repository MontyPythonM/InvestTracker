using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Extensions;

public static class ChronologicalInflationRatesExtensions
{
    public static ChronologicalInflationRates ReduceToDateRange(this ChronologicalInflationRates chronologicalInflationRates, 
        DateOnly from, DateOnly to)
    {
        var reducedInflationRates = chronologicalInflationRates.InflationRates
            .Where(rate => (rate.MonthlyDate.Year > from.Year && rate.MonthlyDate.Year < to.Year) ||
                           (rate.MonthlyDate.Year == to.Year && rate.MonthlyDate.Month < to.Month) ||
                           (rate.MonthlyDate.Year == from.Year && rate.MonthlyDate.Month >= from.Month));

        return new ChronologicalInflationRates(reducedInflationRates);
    }

    public static ChronologicalInflationRates SetZeroInflationRateForDeflation(this ChronologicalInflationRates chronologicalInflationRates)
    {
        var inflationRates = chronologicalInflationRates.InflationRates
            .Select(rate => new InflationRate(rate.Value < 0 ? 0M : rate.Value, rate.Currency, rate.MonthlyDate.Year, rate.MonthlyDate.Month))
            .ToList();
        
        return new ChronologicalInflationRates(inflationRates);
    }
}