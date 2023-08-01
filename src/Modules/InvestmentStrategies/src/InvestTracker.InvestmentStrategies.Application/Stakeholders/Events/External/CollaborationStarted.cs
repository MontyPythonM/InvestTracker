using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record CollaborationStarted(Guid AdvisorId, Guid InvestorId) : IEvent;