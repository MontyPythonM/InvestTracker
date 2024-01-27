using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record UserSubscriptionChanged(Guid Id, string FullName, string Email, string Subscription, Guid ModifiedBy) : IEvent;