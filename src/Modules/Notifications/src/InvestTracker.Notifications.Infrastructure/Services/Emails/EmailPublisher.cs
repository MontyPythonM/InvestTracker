using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Infrastructure.Dto;
using InvestTracker.Notifications.Infrastructure.Interfaces;

namespace InvestTracker.Notifications.Infrastructure.Services.Emails;

internal sealed class EmailPublisher : IEmailPublisher
{
    private readonly IFilteredReceiverRepository _filteredReceiverRepository;
    private readonly IEmailSender _emailSender;

    public EmailPublisher(IFilteredReceiverRepository filteredReceiverRepository, IEmailSender emailSender)
    {
        _filteredReceiverRepository = filteredReceiverRepository;
        _emailSender = emailSender;
    }

    public async Task NotifyAsync(PersonalEmailMessage emailMessage, CancellationToken token = default)
    {
        var receivers = await _filteredReceiverRepository.GetFilteredAsync(emailMessage.Recipients, emailMessage.FilterBySetting, true, token);
        
        var filteredRecipients = receivers
            .Where(r => r.PersonalSettings.EnableEmails)
            .Select(r => r.Email)
            .ToHashSet();

        await _emailSender.SendAsync(new EmailMessage(filteredRecipients, emailMessage.Subject, emailMessage.Body, emailMessage.HtmlBody), token);
    }

    public async Task NotifyAsync(GroupEmailMessage emailMessage, CancellationToken token = default)
    {
        var receivers = await _filteredReceiverRepository.GetFilteredAsync(emailMessage.RecipientGroup, emailMessage.FilterBySetting, true, token);
        var recipients = receivers.Where(r => r.PersonalSettings.EnableEmails);
        
        if (emailMessage.ExcludedReceiverIds is not null)
        {
            recipients = recipients.ExceptBy(emailMessage.ExcludedReceiverIds, r => r.Id);
        }

        var filteredRecipients = recipients.Select(r => r.Email).ToHashSet();
        
        await _emailSender.SendAsync(new EmailMessage(filteredRecipients, emailMessage.Subject, emailMessage.Body, emailMessage.HtmlBody), token);
    }
    
    public async Task NotifyAsync(DirectEmailMessage emailMessage, CancellationToken token = default)
    {
        await _emailSender.SendAsync(new EmailMessage(emailMessage.Recipients, emailMessage.Subject, emailMessage.Body, emailMessage.HtmlBody), token);
    }
}