using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Offers.Api.Controllers;

[ApiController]
[Route(OffersModule.BasePath + "/[controller]")]
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