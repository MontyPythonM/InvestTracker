using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Events;

public record InvestmentStrategyShared(Guid InvestmentStrategyId, string InvestmentStrategyTitle, 
    Guid OwnerId, Guid CollaboratorId) : IEvent;