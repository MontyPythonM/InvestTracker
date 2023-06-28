using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;

internal record RejectInvitation(Guid Id) : ICommand;