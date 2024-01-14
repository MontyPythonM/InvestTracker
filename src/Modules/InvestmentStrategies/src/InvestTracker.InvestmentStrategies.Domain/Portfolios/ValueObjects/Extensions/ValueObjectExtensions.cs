namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Extensions;

public static class ValueObjectExtensions
{
    public static ChronologicalInflationRates ReduceToDateRange(this ChronologicalInflationRates chronologicalInflationRates, 
        DateOnly from, DateOnly to)
    {
        var reducedInflationRates = chronologicalInflationRates.Values
            .Where(rate => (rate.MonthlyDate.Year > from.Year && rate.MonthlyDate.Year < to.Year) ||
                           (rate.MonthlyDate.Year == to.Year && rate.MonthlyDate.Month <= to.Month) ||
                           (rate.MonthlyDate.Year == from.Year && rate.MonthlyDate.Month >= from.Month));

        return new ChronologicalInflationRates(reducedInflationRates);
    }
    
    public static InterestRate GetCumulativeInterestRate(this IEnumerable<InterestRate> interestRates)
        => interestRates.Aggregate<InterestRate, InterestRate>(1, (current, interestRate) => current * (1 + interestRate));
    
    public static ChronologicalInflationRates SetZeroInflationRateForDeflation(this ChronologicalInflationRates chronologicalInflationRates)
    {
        var inflationRates = chronologicalInflationRates.Values
            .Select(rate => new InflationRate(rate.Value < 0 ? 0M : rate.Value, rate.Currency, rate.MonthlyDate.Year, rate.MonthlyDate.Month))
            .ToList();
        
        return new ChronologicalInflationRates(inflationRates);
    }
}