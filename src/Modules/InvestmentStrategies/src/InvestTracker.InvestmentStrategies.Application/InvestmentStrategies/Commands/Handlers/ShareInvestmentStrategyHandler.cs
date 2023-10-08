using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Application.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands.Handlers;

internal sealed class ShareInvestmentStrategyHandler : ICommandHandler<ShareInvestmentStrategy>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IStakeholderRepository _stakeholderRepository;

    public ShareInvestmentStrategyHandler(IInvestmentStrategyRepository investmentStrategyRepository, 
        IRequestContext requestContext, IStakeholderRepository stakeholderRepository)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _requestContext = requestContext;
        _stakeholderRepository = stakeholderRepository;
    }
    
    public async Task HandleAsync(ShareInvestmentStrategy command, CancellationToken token)
    {
        var currentUser = _requestContext.Identity.UserId;
        
        var strategy = await _investmentStrategyRepository.GetAsync(command.InvestmentStrategyId, token);
        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(command.InvestmentStrategyId);
        }
        
        var advisor = await _stakeholderRepository.GetAsync(command.ShareWith, token);
        if (advisor is null)
        {
            throw new StakeholderNotFoundException(command.ShareWith);
        }

        strategy.AssignCollaborator(advisor.Id, currentUser);
        await _investmentStrategyRepository.UpdateAsync(strategy, token);
    }
}