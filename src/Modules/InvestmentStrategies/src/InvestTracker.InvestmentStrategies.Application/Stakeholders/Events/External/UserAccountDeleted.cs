using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External;

public record UserAccountDeleted(Guid Id) : IEvent;