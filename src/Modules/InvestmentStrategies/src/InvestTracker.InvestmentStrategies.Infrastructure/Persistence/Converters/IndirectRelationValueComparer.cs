using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class IndirectRelationValueComparer<T> : ValueComparer<ICollection<T>>
{
    public IndirectRelationValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>) c.ToHashSet())
    {
    }
}