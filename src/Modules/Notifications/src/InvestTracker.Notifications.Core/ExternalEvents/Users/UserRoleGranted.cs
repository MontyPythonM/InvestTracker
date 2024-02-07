using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record UserRoleGranted(Guid Id, string Role, Guid ModifiedBy) : IEvent;