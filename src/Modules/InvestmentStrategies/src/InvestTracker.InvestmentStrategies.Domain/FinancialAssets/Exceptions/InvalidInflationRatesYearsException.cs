using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InvalidInflationRatesYearsException : InvestTrackerException
{
    public InvalidInflationRatesYearsException(Guid id) 
        : base($"Incorrect inflation years were submitted to calculate asset with ID: '{id}'.")
    {
    }
}