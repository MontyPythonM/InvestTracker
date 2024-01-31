using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record AccountDeleted(Guid Id) : IEvent;