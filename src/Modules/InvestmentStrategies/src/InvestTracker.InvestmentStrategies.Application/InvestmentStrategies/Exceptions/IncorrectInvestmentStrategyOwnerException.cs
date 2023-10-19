using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Exceptions;

internal class IncorrectInvestmentStrategyOwnerException : InvestTrackerException
{
    public IncorrectInvestmentStrategyOwnerException(Guid id) : base($"Investment strategy with ID: {id} does not belong to the user.")
    {
    }
}