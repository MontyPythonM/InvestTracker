namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Extensions;

public static class ValueObjectExtensions
{
    public static ChronologicalInflationRates ReduceToTimeScope(this ChronologicalInflationRates chronologicalInflationRates, 
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
}