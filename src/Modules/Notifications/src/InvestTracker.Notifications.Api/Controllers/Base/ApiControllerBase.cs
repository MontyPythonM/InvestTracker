using InvestTracker.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Notifications.Api.Controllers.Base;

[ApiController]
[ApiExplorerSettings(GroupName = NotificationsModule.BasePath)]
[Route(NotificationsModule.BasePath + "/[controller]")]
internal class ApiControllerBase : ControllerBase, IPermissionInjectable
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}