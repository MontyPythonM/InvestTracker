using InvestTracker.Notifications.Core.Dto.Emails;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IEmailSender
{
    ValueTask SendAsync(EmailMessage message, CancellationToken token = default);
}