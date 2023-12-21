using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Seeders;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class DataCollectorsController : ApiControllerBase
{
    private readonly IInflationRateSeeder _inflationRateSeeder;
    private readonly IInflationRateRepository _inflationRateRepository;
    
    public DataCollectorsController(IInflationRateSeeder inflationRateSeeder, IInflationRateRepository inflationRateRepository)
    {
        _inflationRateSeeder = inflationRateSeeder;
        _inflationRateRepository = inflationRateRepository;
    }
    
    [HttpPost("inflation-rates/seed")]
    [HasPermission(InvestmentStrategiesPermission.SeedInflationRates)]
    [SwaggerOperation("Add inflation rates from csv. Seed only not existing rates")]
    public async Task<ActionResult> SeedInflationRates(CancellationToken token)
    {
        return Ok(await _inflationRateSeeder.SeedAsync(false, token));
    }
    
    [HttpPost("inflation-rates/force-seed")]
    [HasPermission(InvestmentStrategiesPermission.ForceSeedInflationRates)]
    [SwaggerOperation("Add inflation rates from csv. Override existing rates")]
    public async Task<ActionResult> ForceSeedInflationRates(CancellationToken token)
    {
        return Ok(await _inflationRateSeeder.SeedAsync(true, token));
    }
}