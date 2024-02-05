using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record AccountDeactivated(Guid Id, Guid ModifiedBy) : IEvent;