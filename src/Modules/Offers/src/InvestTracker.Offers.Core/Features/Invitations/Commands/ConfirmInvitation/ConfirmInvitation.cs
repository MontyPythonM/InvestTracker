using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;

internal record ConfirmInvitation(Guid Id) : ICommand;