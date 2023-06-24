using InvestTracker.Offers.Core.Entities;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Commands.CreateOffer;

internal record CreateOffer(Guid Id, string Title, string Description, decimal? Price, 
    Guid AdvisorId, IEnumerable<string> Tags) : ICommand;