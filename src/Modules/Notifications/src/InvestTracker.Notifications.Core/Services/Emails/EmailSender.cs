using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Core.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace InvestTracker.Notifications.Core.Services.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _options;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(EmailSenderOptions options, ILogger<EmailSender> logger)
    {
        _options = options;
        _logger = logger;
    }

    public async ValueTask SendAsync(EmailMessage message, CancellationToken token = default)
    {
        if (_options.Enabled is false) return;
        
        using var client = new SmtpClient();
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));

            var recipients = _options.UseRedirectTo
                ? GetAddresses(_options.RedirectTo)
                : GetAddresses(message.Recipients.Select(r => r.Value));
        
            mimeMessage.To.AddRange(recipients);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(TextFormat.Plain) { Text = message.Body };
            
            await client.ConnectAsync(_options.Server, _options.Port, _options.UseSsl, token);
            await client.AuthenticateAsync(_options.Username, _options.Password, token);
            await client.SendAsync(mimeMessage, token);
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot send email message. Error message: {errorMessage}", ex.Message);
        }
        finally
        {
            await client.DisconnectAsync(true, token);
        }
    }

    private static IEnumerable<InternetAddress> GetAddresses(IEnumerable<string> emails) 
        => emails.Select(email => new MailboxAddress(string.Empty, email));
}