using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

internal interface IOfferRepository
{
    Task<Offer?> GetAsync(Guid offerId, CancellationToken token);
    Task CreateAsync(Offer offer, CancellationToken token);
    Task UpdateAsync(Offer offer, CancellationToken token);
    Task DeleteAsync(Offer offer, CancellationToken token);
    Task DeleteAsync(IEnumerable<Offer> offers, CancellationToken token);
}