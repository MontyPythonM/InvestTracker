using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record UserAccountActivated(Guid Id, Guid ModifiedBy) : IEvent;