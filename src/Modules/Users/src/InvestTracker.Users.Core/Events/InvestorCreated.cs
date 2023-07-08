using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Users.Core.Events;

public record InvestorCreated(Guid Id, string FullName, string Email) : IEvent;