using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Users.Api.Controllers.Base;

[ApiController]
[ApiExplorerSettings(GroupName = UsersModule.BasePath)]
[Route(UsersModule.BasePath + "/[controller]")]
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