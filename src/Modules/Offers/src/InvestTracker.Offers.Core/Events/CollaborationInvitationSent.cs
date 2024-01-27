using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events;

public record CollaborationInvitationSent(string SenderEmail, string SenderFullName, string ReceiverEmail, 
    string ReceiverFullName, string OfferTitle, string Message) : IEvent;