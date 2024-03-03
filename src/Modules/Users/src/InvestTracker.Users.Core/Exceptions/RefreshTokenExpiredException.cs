using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

public class RefreshTokenExpiredException() : InvestTrackerException("Refresh token has expired");