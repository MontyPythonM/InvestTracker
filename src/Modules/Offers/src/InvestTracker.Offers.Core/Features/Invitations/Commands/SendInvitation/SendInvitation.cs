using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;

internal record SendInvitation(Guid SenderId, Guid OfferId, string message) : ICommand;