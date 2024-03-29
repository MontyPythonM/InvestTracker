﻿using InvestTracker.Shared.Abstractions.Authentication;

namespace InvestTracker.Users.Core.Dtos;

public record AuthenticationResponse(AccessTokenDto AccessToken, RefreshTokenDto? RefreshToken);