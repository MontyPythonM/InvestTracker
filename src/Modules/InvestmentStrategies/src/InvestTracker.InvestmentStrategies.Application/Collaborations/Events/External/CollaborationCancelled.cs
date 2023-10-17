using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External;

public record CollaborationCancelled(Guid AdvisorId, Guid InvestorId) : IEvent;