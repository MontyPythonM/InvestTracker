using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class MoreThanOneIncompletePeriodInGroupedInflationRatesException : InvestTrackerException
{
    public MoreThanOneIncompletePeriodInGroupedInflationRatesException() : base("More than one investment period is incomplete.")
    {
    }
}