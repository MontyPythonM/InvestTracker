using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.CreateOffer;

internal record CreateOffer(string Title, string Description, decimal? Price, IEnumerable<string> Tags) : ICommand;