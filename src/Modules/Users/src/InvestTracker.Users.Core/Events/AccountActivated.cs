using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record AccountActivated(Guid Id, Guid ModifiedBy) : IEvent;