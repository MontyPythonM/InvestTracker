using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record RefreshTokenRevoked(Guid UserId, Guid ModifiedBy) : IEvent;