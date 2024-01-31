using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record AccountActivated(Guid Id, Guid ModifiedBy) : IEvent;