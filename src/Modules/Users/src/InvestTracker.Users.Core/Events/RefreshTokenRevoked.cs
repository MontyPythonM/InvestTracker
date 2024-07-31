using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record RefreshTokenRevoked(Guid UserId, Guid ModifiedBy) : IEvent;