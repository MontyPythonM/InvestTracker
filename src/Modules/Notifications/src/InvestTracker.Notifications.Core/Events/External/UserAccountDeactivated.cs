using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record UserAccountDeactivated(Guid Id, Guid ModifiedBy) : IEvent;