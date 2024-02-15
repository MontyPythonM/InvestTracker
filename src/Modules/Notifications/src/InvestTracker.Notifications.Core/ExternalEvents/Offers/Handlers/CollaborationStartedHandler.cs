using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Offers.Handlers;

public class CollaborationStartedHandler : IEventHandler<CollaborationStarted>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public CollaborationStartedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(CollaborationStarted @event)
    {
        var investor = await _receiverRepository.GetAsync(@event.InvestorId);
        var advisor = await _receiverRepository.GetAsync(@event.AdvisorId);
        
        var notification = new PersonalNotification(
            $"Collaboration between {advisor?.FullName.Value ?? "advisor"} and {investor?.FullName.Value ?? "investor"} started",
            new List<Guid> { @event.AdvisorId, @event.InvestorId },
            r => r.PersonalSettings.NewCollaborationsActivity);

        await _notificationPublisher.PublishAsync(notification);
    }
}