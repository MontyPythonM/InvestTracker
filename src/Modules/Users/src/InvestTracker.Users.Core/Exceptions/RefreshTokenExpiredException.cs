using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

public class RefreshTokenExpiredException() : InvestTrackerException("Your refresh token has expired. Please login again");