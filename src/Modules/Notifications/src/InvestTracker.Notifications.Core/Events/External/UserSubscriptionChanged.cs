using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record UserSubscriptionChanged(Guid Id, string FullName, string Email, string Subscription, Guid ModifiedBy) : IEvent;