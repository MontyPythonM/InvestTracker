using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Calculators.Api.Controllers;

[Route(CalculatorsModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.Calculators API";
}