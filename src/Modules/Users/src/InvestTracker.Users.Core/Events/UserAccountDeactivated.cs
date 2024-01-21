using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record UserAccountDeactivated(Guid Id, Guid ModifiedBy) : IEvent;