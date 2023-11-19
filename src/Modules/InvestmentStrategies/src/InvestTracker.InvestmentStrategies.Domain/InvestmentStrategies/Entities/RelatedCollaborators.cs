namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class RelatedCollaborators
{
    public Guid CollaboratorId { get; private set; }
    
    private RelatedCollaborators()
    {
    }

    internal RelatedCollaborators(Guid collaboratorId)
    {
        CollaboratorId = collaboratorId;
    }
}