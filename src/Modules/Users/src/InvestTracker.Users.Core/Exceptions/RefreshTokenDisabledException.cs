using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class RefreshTokenDisabledException() : InvestTrackerException("Refresh token feature is disabled");