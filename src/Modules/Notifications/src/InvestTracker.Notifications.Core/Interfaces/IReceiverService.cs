using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IReceiverService
{
    Task<PersonalSettingsDto> GetPersonalSettingsAsync(CancellationToken token);
    Task<IEnumerable<ReceiverDto>> GetReceiversAsync(RecipientGroup recipientGroup, CancellationToken token);
    Task SetPersonalSettingsAsync(SetPersonalSettingsDto dto, CancellationToken token);
}