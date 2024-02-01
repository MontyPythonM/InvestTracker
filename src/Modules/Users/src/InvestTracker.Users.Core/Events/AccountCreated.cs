using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record AccountCreated(Guid Id, string FullName, string Email, string Role, string Subscription, string PhoneNumber) : IEvent;