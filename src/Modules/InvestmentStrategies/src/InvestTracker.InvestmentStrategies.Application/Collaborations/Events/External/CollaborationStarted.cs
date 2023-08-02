using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External;

public record CollaborationStarted(Guid AdvisorId, Guid InvestorId) : IEvent;