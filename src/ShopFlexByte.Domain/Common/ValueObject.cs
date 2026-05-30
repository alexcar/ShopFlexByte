namespace ShopFlexByte.Domain.Common;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other) return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents().Aggregate(0, (hash, obj) =>
        HashCode.Combine(hash, obj?.GetHashCode() ?? 0));

    public static bool operator ==(ValueObject? left, ValueObject? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);
}
