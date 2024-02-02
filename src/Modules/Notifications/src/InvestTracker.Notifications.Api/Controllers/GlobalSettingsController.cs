using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class GlobalSettingsController : ApiControllerBase
{
    private readonly IGlobalSettingsService _globalSettingsService;

    public GlobalSettingsController(IGlobalSettingsService globalSettingsService)
    {
        _globalSettingsService = globalSettingsService;
    }

    [HttpGet]
    [HasPermission(NotificationsPermission.GetGlobalSettings)]
    [SwaggerOperation("Returns current global notification settings")]
    public async Task<ActionResult<GlobalSettingsDto>> GetGlobalSettings(CancellationToken token)
    {
        return OkOrNotFound(await _globalSettingsService.GetAsync(token));
    }

    [HttpPut]
    [HasPermission(NotificationsPermission.SetGlobalSettings)]
    [SwaggerOperation("Set global notification settings")]
    public async Task<ActionResult> SetGlobalSettings(SetGlobalSettingsDto dto, CancellationToken token)
    {
        await _globalSettingsService.SetAsync(dto, token);
        return Ok();
    }
}