using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record UserRoleGranted(Guid Id, string Role, Guid ModifiedBy) : IEvent;