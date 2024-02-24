using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class PasswordForgottenHandler : IEventHandler<PasswordForgotten>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IEmailSender _emailSender;

    public PasswordForgottenHandler(IReceiverRepository receiverRepository, IEmailSender emailSender)
    {
        _receiverRepository = receiverRepository;
        _emailSender = emailSender;
    }

    public async Task HandleAsync(PasswordForgotten @event)
    {
        var user = await _receiverRepository.GetAsync(@event.UserId);
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
        
        var email = new EmailMessage(user.Email, subject, body, htmlBody);
        
        await _emailSender.SendAsync(email);
    }
}