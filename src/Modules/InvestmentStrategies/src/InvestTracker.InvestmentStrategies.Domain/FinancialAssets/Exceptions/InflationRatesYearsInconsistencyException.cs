using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InflationRatesYearsInconsistencyException : InvestTrackerException
{
    public InflationRatesYearsInconsistencyException() : base("There is inconsistency between the years in the indicated inflation rates")
    {
    }
}