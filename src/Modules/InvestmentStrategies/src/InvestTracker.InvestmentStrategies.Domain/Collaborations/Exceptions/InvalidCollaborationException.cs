﻿using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Exceptions;

internal class InvalidCollaborationException : InvestTrackerException
{
    public InvalidCollaborationException(StakeholderId stakeholderId) 
        : base($"Cannot create collaboration where advisor and principal is the same (ID: {stakeholderId})")
    {
    }
}