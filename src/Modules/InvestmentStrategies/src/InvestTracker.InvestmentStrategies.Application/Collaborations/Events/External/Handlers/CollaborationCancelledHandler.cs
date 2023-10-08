using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External.Handlers;

internal sealed class CollaborationCancelledHandler : IEventHandler<CollaborationCancelled>
{
    private readonly ICollaborationRepository _collaborationRepository;

    public CollaborationCancelledHandler(ICollaborationRepository collaborationRepository)
    {
        _collaborationRepository = collaborationRepository;
    }

    public async Task HandleAsync(CollaborationCancelled @event)
    {
        var collaboration = await _collaborationRepository.GetAsync(@event.AdvisorId, @event.InvestorId);
        if (collaboration is null)
        {
            return;
        }

        await _collaborationRepository.DeleteAsync(collaboration);
    }
}