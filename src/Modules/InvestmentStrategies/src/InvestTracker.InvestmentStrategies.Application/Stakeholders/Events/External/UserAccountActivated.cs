﻿using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record UserAccountActivated(Guid Id, Guid ModifiedBy) : IEvent;