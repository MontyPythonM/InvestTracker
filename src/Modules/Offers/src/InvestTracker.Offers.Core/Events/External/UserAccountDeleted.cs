using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External;

public record UserAccountDeleted(Guid Id) : IEvent;