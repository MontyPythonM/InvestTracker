using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record UserSubscriptionChanged(Guid Id, string FullName, string Email, string Subscription, Guid ModifiedBy) : IEvent;