using InvestTracker.InvestmentStrategies.Api.Controllers.Base;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.InvestmentStrategies.Api.Controllers;

internal class FinancialAssetsController : ApiControllerBase
{
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;

    protected FinancialAssetsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        CommandDispatcher = commandDispatcher;
        QueryDispatcher = queryDispatcher;
    }
}