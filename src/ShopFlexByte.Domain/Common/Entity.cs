namespace ShopFlexByte.Domain.Common;

public class Entity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? UpdatedAt { get; protected set; }
    public bool Active { get; protected set; }  

    protected Entity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTimeOffset.UtcNow;
        Active = true;
    }

    /// <summary>
    /// Desativa a entidade (soft delete).
    /// </summary>
    public void Deactivate()
    {
        Active = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Reativa a entidade.
    /// </summary>
    public void Activate()
    {
        Active = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Marca a entidade como atualizada.
    /// </summary>
    public void MarkAsUpdated() => UpdatedAt = DateTimeOffset.UtcNow;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
}
