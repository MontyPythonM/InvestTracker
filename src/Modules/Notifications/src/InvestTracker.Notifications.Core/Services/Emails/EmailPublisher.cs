using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;

namespace InvestTracker.Notifications.Core.Services.Emails;

internal sealed class EmailPublisher : IEmailPublisher
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IEmailSender _emailSender;

    public EmailPublisher(IReceiverRepository receiverRepository, IEmailSender emailSender)
    {
        _receiverRepository = receiverRepository;
        _emailSender = emailSender;
    }

    public async Task NotifyAsync(PersonalEmailMessage emailMessage, CancellationToken token = default)
    {
        var receivers = await _receiverRepository.GetFilteredAsync(emailMessage.Recipients, emailMessage.FilterBySetting, true, token);
        
        var filteredRecipients = receivers
            .Where(r => r.PersonalSettings.EnableEmails)
            .Select(r => r.Email)
            .ToHashSet();

        await _emailSender.SendAsync(new EmailMessage(filteredRecipients, emailMessage.Subject, emailMessage.Body), token);
    }

    public async Task NotifyAsync(GroupEmailMessage emailMessage, CancellationToken token = default)
    {
        var receivers = await _receiverRepository.GetFilteredAsync(emailMessage.RecipientGroup, emailMessage.FilterBySetting, true, token);
        var recipients = receivers.Where(r => r.PersonalSettings.EnableEmails);
        
        if (emailMessage.ExcludedReceiverIds is not null)
        {
            recipients = recipients.ExceptBy(emailMessage.ExcludedReceiverIds, r => r.Id);
        }

        var filteredRecipients = recipients.Select(r => r.Email).ToHashSet();
        
        await _emailSender.SendAsync(new EmailMessage(filteredRecipients, emailMessage.Subject, emailMessage.Body), token);
    }
}