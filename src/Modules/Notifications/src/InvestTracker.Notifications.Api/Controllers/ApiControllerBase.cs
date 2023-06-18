using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Notifications.Api.Controllers;

[ApiController]
[Route(NotificationsModule.BasePath + "/[controller]")]
internal class ApiControllerBase : ControllerBase
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