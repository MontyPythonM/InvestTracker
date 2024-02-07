using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Offers;

public record CollaborationCancelled(Guid AdvisorId, Guid InvestorId) : IEvent;