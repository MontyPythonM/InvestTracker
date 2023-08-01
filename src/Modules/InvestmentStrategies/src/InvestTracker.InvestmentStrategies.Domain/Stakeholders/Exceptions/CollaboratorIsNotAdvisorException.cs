using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;

internal class CollaboratorIsNotAdvisorException : InvestTrackerException
{
    public CollaboratorIsNotAdvisorException(StakeholderId id) 
        : base($"Stakeholder with ID: {id} cannot become a collaborator, because advisor subscription is required.")
    {
    }
}