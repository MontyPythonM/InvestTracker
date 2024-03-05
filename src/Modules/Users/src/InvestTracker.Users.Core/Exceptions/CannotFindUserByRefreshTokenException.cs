using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class CannotFindUserByRefreshTokenException(string refreshToken)
    : InvestTrackerException($"Cannot find user with refresh token '{refreshToken}'");