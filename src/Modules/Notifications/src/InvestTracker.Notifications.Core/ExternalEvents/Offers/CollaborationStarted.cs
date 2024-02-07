using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Offers;

public record CollaborationStarted(Guid AdvisorId, Guid InvestorId) : IEvent;