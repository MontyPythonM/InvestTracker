using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record AccountCreated(Guid Id, string FullName, string Email, string Role, string Subscription, string PhoneNumber) : IEvent;