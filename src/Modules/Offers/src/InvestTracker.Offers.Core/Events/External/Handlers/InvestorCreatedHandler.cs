using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class InvestorCreatedHandler : IEventHandler<InvestorCreated>
{
    private readonly IInvestorRepository _investorRepository;

    public InvestorCreatedHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }
    
    public async Task HandleAsync(InvestorCreated @event)
    {
        if (await _investorRepository.ExistsAsync(@event.Id, CancellationToken.None))
        {
            return;
        }
        
        var investor = new Investor(@event.Id, @event.FullName, @event.Email);

        await _investorRepository.CreateAsync(investor, CancellationToken.None);
    }
}