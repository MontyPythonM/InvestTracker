using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.UpdateOffer;

internal record UpdateOffer(Guid Id, string Title, string Description, decimal? Price, IEnumerable<string> Tags) : ICommand;