using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.DeleteOffer;

public record DeleteOffer(Guid Id) : ICommand;