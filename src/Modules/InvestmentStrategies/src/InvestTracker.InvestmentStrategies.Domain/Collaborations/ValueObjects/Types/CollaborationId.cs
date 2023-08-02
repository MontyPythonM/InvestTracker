using InvestTracker.InvestmentStrategies.Domain.Collaborations.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;

public record CollaborationId
{
    public Guid AdvisorId { get; }
    public Guid PrincipalId { get; }
    
    public CollaborationId(StakeholderId advisorId, StakeholderId principalId)
    {
        if (advisorId.Value == principalId.Value)
        {
            throw new InvalidCollaborationException(advisorId);
        }

        AdvisorId = advisorId;
        PrincipalId = principalId;
    }
};