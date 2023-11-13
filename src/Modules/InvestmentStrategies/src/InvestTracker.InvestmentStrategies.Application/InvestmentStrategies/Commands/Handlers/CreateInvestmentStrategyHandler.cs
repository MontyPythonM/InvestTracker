using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands.Handlers;

internal sealed class CreateInvestmentStrategyHandler : ICommandHandler<CreateInvestmentStrategy>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IEnumerable<IStrategyLimitPolicy> _policies;

    public CreateInvestmentStrategyHandler(IInvestmentStrategyRepository investmentStrategyRepository, 
        IRequestContext requestContext, IEnumerable<IStrategyLimitPolicy> policies)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _requestContext = requestContext;
        _policies = policies;
    }

    public async Task HandleAsync(CreateInvestmentStrategy command, CancellationToken token)
    {
        var currentUserId = _requestContext.Identity.UserId;
        var existingUserStrategies = await _investmentStrategyRepository.GetOwnerStrategiesAsync(currentUserId, token);
        
        var strategy = InvestmentStrategy.Create(command.Title, currentUserId, command.Note, 
            _requestContext.Identity.Subscription, _policies, existingUserStrategies);

        await _investmentStrategyRepository.AddAsync(strategy, token);
    }
}