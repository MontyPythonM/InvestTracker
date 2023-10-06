﻿namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects
{
    public abstract class TypeId : IEquatable<TypeId>
    {
        public Guid Value { get; }

        protected TypeId()
        {
        }
        
        protected TypeId(Guid value)
            => Value = value;

        public bool IsEmpty() => Value == Guid.Empty;

        public bool Equals(TypeId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeId) obj);
        }

        public static implicit operator Guid(TypeId typeId)
            => typeId.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(TypeId a, TypeId b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is not null && b is not null)
            {
                return a.Value.Equals(b.Value);
            }

            return false;
        }

        public static bool operator !=(TypeId a, TypeId b) => !(a == b);

        public override string ToString() => Value.ToString();
    }
}