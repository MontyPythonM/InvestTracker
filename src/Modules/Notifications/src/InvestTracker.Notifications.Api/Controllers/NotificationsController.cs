using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Dto;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class NotificationsController : ApiControllerBase
{
    private readonly INotificationPublisher _notificationPublisher;
    private readonly INotificationSender _notificationSender;

    public NotificationsController(INotificationPublisher notificationPublisher, INotificationSender notificationSender)
    {
        _notificationPublisher = notificationPublisher;
        _notificationSender = notificationSender;
    }

    [HttpGet("recipient-groups")]
    [HasPermission(NotificationsPermission.GetRecipientsGroups)]
    [SwaggerOperation("Returns recipients groups used by notifications module")]
    public ActionResult<IEnumerable<ReceiverGroupsDto>> GetRecipientsGroups()
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
    [SwaggerOperation("Send notifications to a selected recipients who are currently connected and have enabled settings")]
    public async Task<ActionResult> SendNotification(SendNotificationDto dto, CancellationToken token)
    {
        var notification = new PersonalNotification(dto.Message, dto.RecipientIds);
        await _notificationPublisher.NotifyAsync(notification, token);
        return Ok();
    }
    
    [HttpPost("send-notification-to-group")]
    [HasPermission(NotificationsPermission.SendNotificationToGroup)]
    [SwaggerOperation("Send notifications to a selected group of recipients who are currently connected and have enabled settings")]
    public async Task<ActionResult> SendNotificationToGroup(SendMessageToGroupDto dto, CancellationToken token)
    {
        var notification = new GroupNotification(dto.Message, dto.RecipientGroup);
        await _notificationPublisher.NotifyAsync(notification, token);
        return Ok();
    }
    
    [HttpPost("send-force-notification")]
    [HasPermission(NotificationsPermission.SendForceNotification)]
    [SwaggerOperation("Send notifications to a selected recipients who are currently connected (ignores their settings)")]
    public async Task<ActionResult> SendForceNotification(SendNotificationDto dto, CancellationToken token)
    {
        await _notificationSender.SendAsync(new Notification(dto.Message, dto.RecipientIds.ToHashSet()), token);
        return Ok();
    }
}