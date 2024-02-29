using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class PasswordForgottenHandler : IEventHandler<PasswordForgotten>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IEmailPublisher _emailPublisher;

    public PasswordForgottenHandler(IReceiverRepository receiverRepository, IEmailPublisher emailPublisher)
    {
        _receiverRepository = receiverRepository;
        _emailPublisher = emailPublisher;
    }

    public async Task HandleAsync(PasswordForgotten @event)
    {
        var user = await _receiverRepository.GetAsync(@event.UserId, true);
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
        
        await _emailPublisher.NotifyAsync(email);
    }
}