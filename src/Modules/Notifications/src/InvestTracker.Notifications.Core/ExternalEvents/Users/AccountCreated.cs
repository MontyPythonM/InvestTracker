﻿using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users;

public record AccountCreated(Guid Id, string FullName, string Email, string Role, string Subscription, string PhoneNumber) : IEvent;