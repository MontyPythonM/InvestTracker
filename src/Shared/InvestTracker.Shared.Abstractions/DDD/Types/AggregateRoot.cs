using InvestTracker.Shared.Abstractions.Auditable;

namespace InvestTracker.Shared.Abstractions.DDD.Types;

public abstract class AggregateRoot : IAuditable
{
    private List<IDomainEvent> _events = new();
    private bool _versionIncremented;
    
    public int Version { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public IEnumerable<IDomainEvent> Events => _events;

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

    protected void SetModification(DateTime modifiedAt, Guid modifiedBy)
    {
        ModifiedAt = modifiedAt;
        ModifiedBy = modifiedBy;
    }
    
    protected void SetCreation(DateTime createdAt, Guid createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }
}

public abstract class AggregateRoot<T> : AggregateRoot
{
    public T Id { get; protected set; }
}