using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class AccountDeletedHandler : IEventHandler<AccountDeleted>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IEmailPublisher _emailPublisher;

    public AccountDeletedHandler(IReceiverRepository receiverRepository, IEmailPublisher emailPublisher)
    {
        _receiverRepository = receiverRepository;
        _emailPublisher = emailPublisher;
    }

    public async Task HandleAsync(AccountDeleted @event)
    {
        var receiver = await _receiverRepository.GetAsync(@event.Id);

        if (receiver is null)
        {
            return;
        }

        const string body = """
                            Hey,
                            your account was successfully deleted from our application. We hope you'll come back to us again someday!
                            
                            Best regards,
                            InvestTracker team
                            """;
        
        var email = new DirectEmailMessage(receiver.Email, "Your InvestTracker account was deleted", body);
        
        await _receiverRepository.DeleteAsync(receiver);
        await _emailPublisher.NotifyAsync(email);
    }
}