using InvestTracker.Offers.Core.Events;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Collaborations.Commands.CancelCollaboration;

internal sealed class CancelCollaborationHandler : ICommandHandler<CancelCollaboration>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly ITime _time;

    public CancelCollaborationHandler(IMessageBroker messageBroker,
        ICollaborationRepository collaborationRepository, ITime time)
    {
        _messageBroker = messageBroker;
        _collaborationRepository = collaborationRepository;
        _time = time;
    }
    
    public async Task HandleAsync(CancelCollaboration command, CancellationToken token)
    {
        var collaboration = await _collaborationRepository.GetAsync(command.AdvisorId, command.InvestorId, token);

        if (collaboration is null)
        {
            throw new CollaborationNotFoundException(command.AdvisorId, command.InvestorId);
        }

        collaboration.IsCancelled = true;
        collaboration.CancelledAt = _time.Current();
        
        await _collaborationRepository.UpdateAsync(collaboration, token);
        await _messageBroker.PublishAsync(new CollaborationCancelled(collaboration.AdvisorId, collaboration.InvestorId));
    }
}