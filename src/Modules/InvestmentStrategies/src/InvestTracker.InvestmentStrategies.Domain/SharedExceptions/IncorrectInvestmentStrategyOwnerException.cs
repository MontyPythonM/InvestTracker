using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class IncorrectInvestmentStrategyOwnerException : InvestTrackerException
{
    public IncorrectInvestmentStrategyOwnerException(StakeholderId id) : base($"Investment strategy with ID: {id.Value} does not belong to the user.")
    {
    }
}