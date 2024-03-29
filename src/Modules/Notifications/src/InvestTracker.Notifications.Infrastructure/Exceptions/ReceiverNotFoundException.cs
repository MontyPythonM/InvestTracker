﻿using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Notifications.Infrastructure.Exceptions;

internal sealed class ReceiverNotFoundException : InvestTrackerException
{
    public ReceiverNotFoundException(Guid receiverId) : base($"Receiver with ID: '{receiverId}' not found")
    {
    }
}