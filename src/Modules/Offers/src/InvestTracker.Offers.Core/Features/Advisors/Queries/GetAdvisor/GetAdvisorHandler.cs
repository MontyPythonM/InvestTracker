using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;
using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Advisors.Queries.GetAdvisor;

internal sealed class GetAdvisorHandler : IQueryHandler<GetAdvisor, AdvisorDetailsDto>
{
    private readonly OffersDbContext _context;
    private readonly IRequestContext _requestContext;
    
    public GetAdvisorHandler(OffersDbContext context, IRequestContext requestContext)
    {
        _context = context;
        _requestContext = requestContext;
    }
    
    public async Task<AdvisorDetailsDto> HandleAsync(GetAdvisor query, CancellationToken token = default)
    {
        var advisorId = query.Id;
        var currentUser = _requestContext.Identity;
        
        if (advisorId != currentUser.UserId && !SystemRole.IsAdministrator(currentUser.Role))
            throw new AdvisorAccessException(advisorId);
        
        var advisor = await _context.Advisors
            .AsNoTracking()
            .Select(advisor => new AdvisorDetailsDto
            {
                Id = advisor.Id,
                FullName = advisor.FullName,
                Email = advisor.Email,
                Bio = advisor.Bio,
                PhoneNumber = advisor.PhoneNumber,
                CompanyName = advisor.CompanyName,
                Avatar = advisor.Avatar == null ? null : Convert.ToBase64String(advisor.Avatar) 
            })
            .SingleOrDefaultAsync(advisor => advisor.Id == advisorId, token);
        
        if (advisor is null)
            throw new AdvisorNotFoundException(advisorId);

        return advisor;
    }
}