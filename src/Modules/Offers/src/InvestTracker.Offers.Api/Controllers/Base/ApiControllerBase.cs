using InvestTracker.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Offers.Api.Controllers.Base;

[ApiController]
[ApiExplorerSettings(GroupName = OffersModule.BasePath)]
[Route(OffersModule.BasePath + "/[controller]")]
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