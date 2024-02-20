using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class AccountDeletedHandler : IEventHandler<AccountDeleted>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IEmailSender _emailSender;

    public AccountDeletedHandler(IReceiverRepository receiverRepository, IEmailSender emailSender)
    {
        _receiverRepository = receiverRepository;
        _emailSender = emailSender;
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
        
        var email = new EmailMessage(receiver.Email, "Your InvestTracker account was deleted", body);
        
        await _receiverRepository.DeleteAsync(receiver);
        await _emailSender.SendAsync(email);
    }
}