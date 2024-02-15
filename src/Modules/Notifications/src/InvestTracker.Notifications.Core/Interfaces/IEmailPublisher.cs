using InvestTracker.Notifications.Core.Dto.Emails;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IEmailPublisher
{
    Task PublishAsync(PersonalEmailMessage emailMessage, CancellationToken token = default);
    Task PublishAsync(GroupEmailMessage emailMessage, CancellationToken token = default);
}