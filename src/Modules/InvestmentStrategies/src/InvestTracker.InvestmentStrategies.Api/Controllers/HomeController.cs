using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

[Route(InvestmentStrategiesModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    private readonly IExchangeRateSeeder _exchangeRateSeeder;
    
    public HomeController(IExchangeRateSeeder exchangeRateSeeder)
    {
        _exchangeRateSeeder = exchangeRateSeeder;
    }
    
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.InvestmentStrategies API";

    [HttpGet("seed")]
    public async Task<ActionResult> Seed(CancellationToken token)
    {
        await _exchangeRateSeeder.SeedAsync(token);
        return Ok();
    }
}