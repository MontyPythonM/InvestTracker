using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record UserRoleRemoved(Guid Id, Guid ModifiedBy) : IEvent;