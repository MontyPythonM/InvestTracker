using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class OwnerIsNotPrincipalOfCollaborationException : InvestTrackerException
{
    public OwnerIsNotPrincipalOfCollaborationException(InvestmentStrategyId investmentStrategyId) 
        : base($"Cannot add collaboration in investment strategy with ID: {investmentStrategyId.Value}, because collaboration principal and strategy owner is different persons.")
    {
    }
}