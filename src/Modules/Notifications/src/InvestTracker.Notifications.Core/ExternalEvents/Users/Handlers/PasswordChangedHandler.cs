using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class PasswordChangedHandler(
    IReceiverRepository receiverRepository, 
    IEmailPublisher emailPublisher)
    : IEventHandler<PasswordChanged>
{
    public async Task HandleAsync(PasswordChanged @event)
    {
        var user = await receiverRepository.GetAsync(@event.UserId, true);
        if (user is null)
        {
            return;
        }

        var subject = "Invest Tracker - Password reset";
        
        var body = $"""
                    Hi {user.FullName.Value},

                    We would like to inform you that your password has been changed.
                    
                    Best regards,
                    InvestTracker team
                    """;
        
        var email = new PersonalEmailMessage(user.Id, subject, body);
        
        await emailPublisher.NotifyAsync(email);
    }
}