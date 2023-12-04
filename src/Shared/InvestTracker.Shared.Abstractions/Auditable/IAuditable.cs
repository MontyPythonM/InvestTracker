namespace InvestTracker.Shared.Abstractions.Auditable;

public interface IAuditable
{
    public DateTime CreatedAt { get; }
    public Guid CreatedBy { get; }
    public DateTime? ModifiedAt { get; }
    public Guid? ModifiedBy { get; }
}