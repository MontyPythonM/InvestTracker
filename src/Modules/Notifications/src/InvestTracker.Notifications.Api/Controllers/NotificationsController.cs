using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Dto;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class NotificationsController : ApiControllerBase
{
    private readonly INotificationPublisher _notificationPublisher;

    public NotificationsController(INotificationPublisher notificationPublisher)
    {
        _notificationPublisher = notificationPublisher;
    }

    [HttpGet("recipient-groups")]
    [HasPermission(NotificationsPermission.GetRecipientsGroups)]
    [SwaggerOperation("")]
    public async Task<ActionResult<IEnumerable<ReceiverGroupsDto>>> GetRecipientsGroups()
    {
        var results = new List<ReceiverGroupsDto>();
        foreach (var group in Enum.GetValues(typeof(RecipientGroup)))
        {
            results.Add(new ReceiverGroupsDto
            {
                Value = (int)group,
                Name = group.ToString() ?? string.Empty
            });
        }

        return results;
    }
    
    [HttpPost("send-notification")]
    [HasPermission(NotificationsPermission.SendNotification)]
    [SwaggerOperation("Send notifications to a selected recipients who are currently connected")]
    public async Task<ActionResult> SendNotification(SendMessageDto dto)
    {
        await _notificationPublisher.PublishAsync(new Notification(dto.Message, dto.RecipientIds.ToHashSet()));
        return Ok();
    }
}