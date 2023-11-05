using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InvalidInterestRateException : InvestTrackerException
{
    public InvalidInterestRateException(decimal interestRate) : base($"InterestRate value: '{interestRate}' is invalid.")
    {
    }
}