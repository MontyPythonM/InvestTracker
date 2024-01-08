using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Calculators.Api.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = CalculatorsModule.BasePath)]
[Route(CalculatorsModule.BasePath + "/[controller]")]
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