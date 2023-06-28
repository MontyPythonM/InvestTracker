using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events;

public record CollaborationStarted(Guid AdvisorId, Guid InvestorId) : IEvent;