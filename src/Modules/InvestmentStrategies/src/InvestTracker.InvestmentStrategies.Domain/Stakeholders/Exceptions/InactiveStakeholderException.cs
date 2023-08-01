using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;

internal class InactiveStakeholderException : InvestTrackerException
{
    public InactiveStakeholderException(StakeholderId id) : base($"Stakeholder with ID: {id} is not active user.")
    {
    }
}