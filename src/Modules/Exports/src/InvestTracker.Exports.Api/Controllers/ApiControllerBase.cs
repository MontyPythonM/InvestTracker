using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Exports.Api.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = ExportsModule.BasePath)]
[Route(ExportsModule.BasePath + "/[controller]")]
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