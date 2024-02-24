using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record PasswordChanged(Guid UserId) : IEvent;