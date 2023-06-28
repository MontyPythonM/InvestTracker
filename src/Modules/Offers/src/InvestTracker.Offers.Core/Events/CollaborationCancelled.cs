using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events;

public record CollaborationCancelled(Guid AdvisorId, Guid InvestorId) : IEvent;