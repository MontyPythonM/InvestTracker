using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record AccountDeactivated(Guid Id, Guid ModifiedBy) : IEvent;