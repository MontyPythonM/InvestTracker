using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External;

public record InvestorAppointed(Guid Id, string FullName, string Email) : IEvent;