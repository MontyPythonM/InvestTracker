using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class StakeholderNotFoundException : InvestTrackerException
{
    public StakeholderNotFoundException(StakeholderId stakeholderId) : base($"Stakeholder with ID: {stakeholderId} not found.")
    {
    }
}