using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class InvalidRefreshTokenException() : InvestTrackerException("Invalid refresh token");