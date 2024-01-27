using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InvestTracker.Shared.Infrastructure.Postgres.Interceptors;

internal sealed class AuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ITimeProvider _timeProvider;
    private readonly IRequestContext _requestContext;

    public AuditableEntitiesInterceptor(ITimeProvider timeProvider, IRequestContext requestContext)
    {
        _timeProvider = timeProvider;
        _requestContext = requestContext;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var now = _timeProvider.Current();
        var author = _requestContext?.Identity?.UserId ?? Guid.Empty;
        
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<IAuditable>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                var createdAtProperty = entityEntry.Property(nameof(IAuditable.CreatedAt));
                var createdByProperty = entityEntry.Property(nameof(IAuditable.CreatedBy));
                
                createdByProperty.CurrentValue = author == Guid.Empty ? createdByProperty.CurrentValue : author;
                createdAtProperty.CurrentValue = now;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                var modifiedAtProperty = entityEntry.Property(nameof(IAuditable.ModifiedAt));
                var modifiedByProperty = entityEntry.Property(nameof(IAuditable.ModifiedBy));

                modifiedByProperty.CurrentValue = author == Guid.Empty ? modifiedByProperty.CurrentValue : author;
                modifiedAtProperty.CurrentValue = now;
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}