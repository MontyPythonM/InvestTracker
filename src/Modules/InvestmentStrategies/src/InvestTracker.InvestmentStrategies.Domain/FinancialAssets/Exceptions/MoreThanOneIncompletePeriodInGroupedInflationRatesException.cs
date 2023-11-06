using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class MoreThanOneIncompletePeriodInGroupedInflationRatesException : InvestTrackerException
{
    public MoreThanOneIncompletePeriodInGroupedInflationRatesException() : base("More than one investment period is incomplete.")
    {
    }
}