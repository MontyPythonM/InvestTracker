using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Extensions;

public static class InterestRateExtensions
{
    public static InterestRate GetCumulativeInterestRate(this IEnumerable<InterestRate> interestRates)
        => interestRates.Aggregate<InterestRate, InterestRate>(1, (current, interestRate) => current * (1 + interestRate));
}