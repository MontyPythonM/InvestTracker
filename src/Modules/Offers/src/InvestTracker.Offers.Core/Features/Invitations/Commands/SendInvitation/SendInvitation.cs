using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;

internal record SendInvitation(Guid OfferId, string Message) : ICommand;