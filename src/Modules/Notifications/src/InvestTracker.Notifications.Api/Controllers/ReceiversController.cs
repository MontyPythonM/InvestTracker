using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class ReceiversController : ApiControllerBase
{
    private readonly IReceiverService _receiverService;

    public ReceiversController(IReceiverService receiverService)
    {
        _receiverService = receiverService;
    }
    
    [HttpGet("group")]
    [HasPermission(NotificationsPermission.GetReceivers)]
    [SwaggerOperation("Returns receivers with their personal notification settings from selected recipient group")]
    public async Task<ActionResult<IEnumerable<PersonalSettingsDto>>> GetReceivers(RecipientGroup group, CancellationToken token)
    {
        return Ok(await _receiverService.GetReceiversAsync(group, token));
    }
}