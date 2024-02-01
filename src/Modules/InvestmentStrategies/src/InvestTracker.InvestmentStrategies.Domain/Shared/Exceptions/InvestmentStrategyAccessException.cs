using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public sealed class InvestmentStrategyAccessException : InvestTrackerException
{
    public InvestmentStrategyAccessException(InvestmentStrategyId id) 
        : base($"Current user does not have access to investment strategy with ID: '{id.Value}' either as an owner or a collaborator")
    {
    }
}