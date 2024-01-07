using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

[ApiController]
[Route(InvestmentStrategiesModule.BasePath + "/financial-assets")]
internal class FinancialAssetsController : ControllerBase
{
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;

    protected FinancialAssetsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        CommandDispatcher = commandDispatcher;
        QueryDispatcher = queryDispatcher;
    }
}