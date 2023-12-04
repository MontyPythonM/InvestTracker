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
                entityEntry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = now;
                entityEntry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = author;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditable.ModifiedAt)).CurrentValue = now;
                entityEntry.Property(nameof(IAuditable.ModifiedBy)).CurrentValue = author;
            }
        }   
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}