using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record InvestorCreated(Guid Id, string FullName, string Email) : IEvent;