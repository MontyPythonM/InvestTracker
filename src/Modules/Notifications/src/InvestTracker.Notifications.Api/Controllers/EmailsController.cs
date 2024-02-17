using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class EmailsController : ApiControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailPublisher _emailPublisher;

    public EmailsController(IEmailSender emailSender, IEmailPublisher emailPublisher)
    {
        _emailSender = emailSender;
        _emailPublisher = emailPublisher;
    }
    
    [HttpPost("send-email")]
    [HasPermission(NotificationsPermission.SendEmail)]
    [SwaggerOperation("Send email to selected email address")]
    public async Task<ActionResult> SendEmail(string emailAddress, string subject, string body, CancellationToken token)
    {
        await _emailSender.SendAsync(new EmailMessage(emailAddress, subject, body), token);
        return Ok();
    }
}