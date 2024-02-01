using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record UserRoleRemoved(Guid Id, Guid ModifiedBy) : IEvent;