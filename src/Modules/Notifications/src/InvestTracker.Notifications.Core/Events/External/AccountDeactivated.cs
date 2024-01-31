using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record AccountDeactivated(Guid Id, Guid ModifiedBy) : IEvent;