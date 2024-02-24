using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record PasswordForgotten(Guid UserId, string ResetPasswordUri) : IEvent;