using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record UserRoleGranted(Guid Id, string Role) : IEvent;