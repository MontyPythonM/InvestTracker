using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Collaborations.Commands.CancelCollaboration;

internal record CancelCollaboration(Guid AdvisorId, Guid InvestorId) : ICommand;
