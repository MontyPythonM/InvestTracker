using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Queries;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));

        if (handleMethod is null)
        {
            throw new InvalidOperationException($"No handle method found for query type '{query.GetType()}' and result type '{typeof(TResult)}'.");
        }
        
        var handleTask = (Task<TResult>)handleMethod.Invoke(handler, new object[] { query, token })!;

        return await handleTask.ConfigureAwait(false);
    }
}