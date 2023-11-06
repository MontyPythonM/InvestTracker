using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class DataCollectorsController : ApiControllerBase
{
    private readonly IExchangeRateSeeder _exchangeRateSeeder;
    
    public DataCollectorsController(IExchangeRateSeeder exchangeRateSeeder)
    {
        _exchangeRateSeeder = exchangeRateSeeder;
    }

    [HttpPost("safe-seed-exchange-rate")]
    [HasPermission(InvestmentStrategiesPermission.SafeSeedExchangeRate)]
    [SwaggerOperation("Seed exchange rate if there are no records in database")]
    public async Task<ActionResult> SaveSeedExchangeRate()
    {
        await _exchangeRateSeeder.SeedAsync();
        return Ok();
    }
    
    [HttpPost("force-seed-exchange-rate")]
    [HasPermission(InvestmentStrategiesPermission.ForceSeedExchangeRate)]
    [SwaggerOperation("Force seed exchange rate even if there are records in database")]
    public async Task<ActionResult> ForceSeedExchangeRate(CancellationToken token)
    {
        await _exchangeRateSeeder.SeedAsync(true, token);
        return Ok();
    }
}