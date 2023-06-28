using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.CreateOffer;

internal record CreateOffer(Guid Id, string Title, string Description, decimal? Price, 
    Guid AdvisorId, IEnumerable<string> Tags) : ICommand;