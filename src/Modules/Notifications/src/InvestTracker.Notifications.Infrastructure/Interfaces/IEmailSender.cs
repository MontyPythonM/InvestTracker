using InvestTracker.Notifications.Infrastructure.Dto;

namespace InvestTracker.Notifications.Infrastructure.Interfaces;

internal interface IEmailSender
{
    ValueTask SendAsync(EmailMessage message, CancellationToken token = default);
}