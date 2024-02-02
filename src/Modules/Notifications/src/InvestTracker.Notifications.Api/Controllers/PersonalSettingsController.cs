using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class PersonalSettingsController : ApiControllerBase
{
    private readonly IReceiverService _receiverService;

    public PersonalSettingsController(IReceiverService receiverService)
    {
        _receiverService = receiverService;
    }

    [HttpGet]
    [HasPermission(NotificationsPermission.GetPersonalSettings)]
    [SwaggerOperation("Returns current user personal notification settings")]
    public async Task<ActionResult<PersonalSettingsDto>> GetPersonalSettings(CancellationToken token)
    {
        return OkOrNotFound(await _receiverService.GetPersonalSettingsAsync(token));
    }

    [HttpPut]
    [HasPermission(NotificationsPermission.SetPersonalSettings)]
    [SwaggerOperation("Set current user personal notification settings")]
    public async Task<ActionResult> SetPersonalSettings(SetPersonalSettingsDto dto, CancellationToken token)
    {
        await _receiverService.SetPersonalSettingsAsync(dto, token);
        return Ok();
    }
}