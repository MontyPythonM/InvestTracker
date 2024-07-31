using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class PasswordForgottenHandler(
    IReceiverRepository receiverRepository,
    IEmailPublisher emailPublisher,
    INotificationPublisher notificationPublisher)
    : IEventHandler<PasswordForgotten>
{
    public async Task HandleAsync(PasswordForgotten @event)
    {
        var user = await receiverRepository.GetAsync(@event.UserId, true);
        if (user is null)
        {
            return;
        }

        var subject = "Invest Tracker - Password reset";
        
        var body = $"""
                    Hi {user.FullName.Value},

                    A password reset action has been performed for your InvestTracker account. Do not take any action if you are not the one who triggered the action.

                    Click here to reset your password:
                    {@event.ResetPasswordUri}

                    Best regards,
                    InvestTracker team
                    """;
        
        var htmlBody = $"""
                   Hi {user.FullName.Value},
                   <br><br>
                   A password reset action has been performed for your InvestTracker account. <b>Do not take any action</b> if you are not the one who triggered the action.
                   <br><br>
                   Click here to reset your password:
                   <a href="{@event.ResetPasswordUri}">Reset password</a>
                   <br><br>
                   Best regards,
                   InvestTracker team
                   """;
        
        var email = new DirectEmailMessage(user.Email, subject, body, htmlBody);
        var notification = new PersonalNotification("Email was sent with a link to reset your password. Check your inbox", user.Id);
        
        await emailPublisher.NotifyAsync(email);
        await notificationPublisher.NotifyAsync(notification);
    }
}