using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Offers.Handlers;

public class CollaborationStartedHandler : IEventHandler<CollaborationStarted>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;
    private readonly IEmailPublisher _emailPublisher;

    public CollaborationStartedHandler(IReceiverRepository receiverRepository, 
        INotificationPublisher notificationPublisher, IEmailPublisher emailPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
        _emailPublisher = emailPublisher;
    }
    
    public async Task HandleAsync(CollaborationStarted @event)
    {
        var investor = await _receiverRepository.GetAsync(@event.InvestorId, true);
        var advisor = await _receiverRepository.GetAsync(@event.AdvisorId, true);

        var receivers = new List<Guid> { @event.AdvisorId, @event.InvestorId };
        
        var advisorName = advisor?.FullName.Value ?? @event.AdvisorId.ToString();
        var investorName = investor?.FullName.Value ?? @event.InvestorId.ToString();
        var message = $"Collaboration between {advisorName} and {investorName} started";
        
        var notification = new PersonalNotification(message, receivers, r => r.NewCollaborationsActivity);
        
        var email = new PersonalEmailMessage(receivers, "InvestTracker - collaboration started", message, 
            setting => setting.NewCollaborationsActivity);

        await _notificationPublisher.NotifyAsync(notification);
        await _emailPublisher.NotifyAsync(email);
    }
}