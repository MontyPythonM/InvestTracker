using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Exports.Api.Controllers;

[Route(ExportsModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.Exports API";
}