﻿using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Offers.Core.Queries.GetOfferDetails.Dtos;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Queries.GetOfferDetails;

internal sealed class GetOfferDetailsHandler : IQueryHandler<GetOfferDetails, OfferDetailsDto>
{
    private readonly OffersDbContext _context;

    public GetOfferDetailsHandler(OffersDbContext context)
    {
        _context = context;
    }

    public async Task<OfferDetailsDto> HandleAsync(GetOfferDetails query, CancellationToken token = default)
    {
        var offerDetails = await _context.Offers
            .AsNoTracking()
            .Include(offer => offer.Advisor)
            .Include(offer => offer.Tags)
            .Select(entity => new OfferDetailsDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Advisor = new AdvisorDetailsDto
                {
                    Id = entity.Advisor.Id,
                    FullName = entity.Advisor.FullName,
                    Email = entity.Advisor.Email,
                    PhoneNumber = entity.Advisor.PhoneNumber,
                    Bio = entity.Advisor.Bio,
                    CompanyName = entity.Advisor.CompanyName,
                    AvatarUrl = entity.Advisor.AvatarUrl
                },
                Tags = entity.Tags
                    .Select(tag => new OfferTagDto()
                    {
                        Id = tag.Id,
                        Value = tag.Value
                    })
                    .ToList()
            })
            .SingleOrDefaultAsync(offer => offer.Id == query.Id, token);

        if (offerDetails is null)
        {
            throw new OfferNotFoundException(query.Id);
        }

        return offerDetails;
    }
}