﻿using InvestTracker.Notifications.Infrastructure.Dto;
using InvestTracker.Notifications.Infrastructure.Exceptions;
using InvestTracker.Notifications.Infrastructure.Interfaces;
using InvestTracker.Notifications.Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace InvestTracker.Notifications.Infrastructure.Services.Emails;

internal sealed class SmtpEmailSender : IEmailSender
{
    private readonly EmailSenderOptions _options;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(EmailSenderOptions options, ILogger<SmtpEmailSender> logger)
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
        
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message.Body;

            if (!string.IsNullOrWhiteSpace(message.HtmlBody))
            {
                bodyBuilder.HtmlBody = message.HtmlBody;
            }

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.To.AddRange(recipients);
            mimeMessage.Subject = message.Subject;
            
            await client.ConnectAsync(_options.Server, _options.Port, _options.UseSsl, token);
            await client.AuthenticateAsync(_options.Username, _options.Password, token);
            await client.SendAsync(mimeMessage, token);
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot send email message. Error message: {errorMessage}, Stack trace: {ex}", ex.Message, ex);
            throw new EmailSenderException($"Cannot send email message. Error message: {ex.Message}");
        }
        finally
        {
            await client.DisconnectAsync(true, token);
        }
    }

    private static IEnumerable<InternetAddress> GetAddresses(IEnumerable<string> emails) 
        => emails.Select(email => new MailboxAddress(string.Empty, email));
}