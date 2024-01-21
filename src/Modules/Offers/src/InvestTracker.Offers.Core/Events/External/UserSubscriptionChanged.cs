using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External;

public record UserSubscriptionChanged(Guid Id, string FullName, string Email, string Subscription, Guid ModifiedBy) : IEvent;