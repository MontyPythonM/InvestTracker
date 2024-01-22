using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record UserAccountActivated(Guid Id, Guid ModifiedBy) : IEvent;