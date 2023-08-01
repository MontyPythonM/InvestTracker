using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Exceptions;

internal class StakeholderNotFoundException : InvestTrackerException
{
    public StakeholderNotFoundException(Guid stakeholderId) 
        : base($"Stakeholder with ID: {stakeholderId} not found.")
    {
    }
}