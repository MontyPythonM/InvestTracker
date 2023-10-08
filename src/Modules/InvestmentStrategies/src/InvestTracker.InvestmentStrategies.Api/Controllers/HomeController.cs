using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

[Route(InvestmentStrategiesModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.InvestmentStrategies API";
}