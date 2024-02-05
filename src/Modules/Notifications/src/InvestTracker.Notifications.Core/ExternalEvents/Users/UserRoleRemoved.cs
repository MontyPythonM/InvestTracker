using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record UserRoleRemoved(Guid Id, Guid ModifiedBy) : IEvent;