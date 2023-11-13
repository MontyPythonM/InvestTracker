using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class InvestmentStrategyAccessException : InvestTrackerException
{
    public InvestmentStrategyAccessException(InvestmentStrategyId id) 
        : base($"Current user does not have access to investment strategy with ID: '{id}' either as an owner or a contributor")
    {
    }
}