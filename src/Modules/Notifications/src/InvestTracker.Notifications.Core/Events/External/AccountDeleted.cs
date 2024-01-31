using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record AccountDeleted(Guid Id) : IEvent;