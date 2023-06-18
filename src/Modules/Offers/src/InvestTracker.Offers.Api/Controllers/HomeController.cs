using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Offers.Api.Controllers;

[Route(OffersModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "Offers API";
}