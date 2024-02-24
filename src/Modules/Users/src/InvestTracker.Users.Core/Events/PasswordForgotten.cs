using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record PasswordForgotten(Guid UserId, string ResetPasswordUri) : IEvent;