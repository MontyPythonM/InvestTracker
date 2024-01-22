using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External;

public record InvestorCreated(Guid Id, string FullName, string Email, string PhoneNumber) : IEvent;