using InvestTracker.Notifications.Core.Dto.Emails;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IEmailPublisher
{
    Task NotifyAsync(PersonalEmailMessage emailMessage, CancellationToken token = default);
    Task NotifyAsync(GroupEmailMessage emailMessage, CancellationToken token = default);
    Task NotifyAsync(DirectEmailMessage emailMessage, CancellationToken token = default);
}