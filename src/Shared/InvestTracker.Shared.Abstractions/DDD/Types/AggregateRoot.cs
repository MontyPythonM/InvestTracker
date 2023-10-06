namespace InvestTracker.Shared.Abstractions.DDD.Types;

public abstract class AggregateRoot
{
    public int Version { get; private set; }
    public IEnumerable<IDomainEvent> Events => _events;
    
    private List<IDomainEvent> _events = new();
    private bool _versionIncremented;
    
    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any())
        {
            IncrementVersion();
        }

        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        Version++;
        _versionIncremented = true;
    }
}

public abstract class AggregateRoot<T> : AggregateRoot
{
    public T Id { get; protected set; }
}