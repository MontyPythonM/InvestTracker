using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record AccountActivated(Guid Id, Guid ModifiedBy) : IEvent;