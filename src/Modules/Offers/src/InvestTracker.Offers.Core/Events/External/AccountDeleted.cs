using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External;

public record AccountDeleted(Guid Id) : IEvent;