using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Commands.DeleteOffer;

public record DeleteOffer(Guid Id) : ICommand;