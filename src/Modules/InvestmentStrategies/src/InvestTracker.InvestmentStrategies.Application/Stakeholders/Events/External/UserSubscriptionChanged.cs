using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record UserSubscriptionChanged(Guid Id, string FullName, string Email, string Subscription) : IEvent;