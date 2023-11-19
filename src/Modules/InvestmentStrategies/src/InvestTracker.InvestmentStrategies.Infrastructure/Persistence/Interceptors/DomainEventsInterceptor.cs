using InvestTracker.Shared.Abstractions.DDD;
using InvestTracker.Shared.Abstractions.DDD.Types;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Interceptors;

internal sealed class DomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DomainEventsInterceptor(IDomainEventDispatcher domainEventDispatcher)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var domainEvents = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(root =>
            {
                var events = root.Events.ToList();
                root.ClearEvents();
                return events;
            })
            .ToArray();

        _domainEventDispatcher.DispatchAsync(domainEvents);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}