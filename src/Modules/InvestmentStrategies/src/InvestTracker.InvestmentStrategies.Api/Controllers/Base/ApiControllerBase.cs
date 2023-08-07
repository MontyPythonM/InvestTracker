using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.InvestmentStrategies.Api.Controllers.Base;

[ApiController]
[Route(InvestmentStrategiesModule.BasePath + "/[controller]")]
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