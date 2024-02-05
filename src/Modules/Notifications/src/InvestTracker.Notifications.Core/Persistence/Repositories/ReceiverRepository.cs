using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence.Repositories;

internal sealed class ReceiverRepository : IReceiverRepository
{
    private readonly NotificationsDbContext _dbContext;

    public ReceiverRepository(NotificationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Receiver?> GetAsync(Guid id, Expression<Func<Receiver, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var query = _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .AsQueryable();

        if (filterBy is not null)
        {
            query = query.Where(filterBy);
        }

        return await query.SingleOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Receiver>> GetAsync(IEnumerable<Guid> receiversIds, Expression<Func<Receiver, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var query = _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .Where(r => receiversIds.Contains(r.Id))
            .AsQueryable();

        if (filterBy is not null)
        {
            query = query.Where(filterBy);
        }
        
        return await query.ToListAsync(token);
    }

    public async Task<IEnumerable<Receiver>> GetAsync(RecipientGroup recipientGroup, Expression<Func<Receiver, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var query = _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .AsQueryable();

        if (filterBy is not null)
        {
            query = query.Where(filterBy);
        }
        
        var filteredQuery = recipientGroup switch
        {
            RecipientGroup.None 
                => null,
            RecipientGroup.StandardInvestors 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor),
            RecipientGroup.ProfessionalInvestors 
                => query.Where(r => r.Subscription == SystemSubscription.ProfessionalInvestor),
            RecipientGroup.Investors 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor || r.Subscription == SystemSubscription.ProfessionalInvestor),
            RecipientGroup.Advisors 
                => query.Where(r => r.Subscription == SystemSubscription.Advisor),
            RecipientGroup.Subscribers 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor || r.Subscription == SystemSubscription.ProfessionalInvestor || r.Subscription == SystemSubscription.Advisor),
            RecipientGroup.BusinessAdministrators 
                => query.Where(r => r.Role == SystemRole.BusinessAdministrator),
            RecipientGroup.SystemAdministrators 
                => query.Where(r => r.Role == SystemRole.SystemAdministrator),
            RecipientGroup.Administrators 
                => query.Where(r => r.Role == SystemRole.BusinessAdministrator || r.Role == SystemRole.SystemAdministrator),
            RecipientGroup.All => query,
            _ => throw new ArgumentOutOfRangeException(nameof(recipientGroup), recipientGroup, null)
        };
        
        return filteredQuery is null 
            ? new List<Receiver>() 
            : await filteredQuery.ToListAsync(token);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .AsNoTracking()
            .AnyAsync(r => r.Id == id, token);
    }

    public async Task CreateAsync(Receiver receiver, CancellationToken token = default)
    {
        await _dbContext.Receivers.AddAsync(receiver, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Receiver receiver, CancellationToken token = default)
    {
        _dbContext.Receivers.Update(receiver);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Receiver receiver, CancellationToken token = default)
    {
        _dbContext.Receivers.Remove(receiver);
        await _dbContext.SaveChangesAsync(token);
    }
}