namespace ShopFlexByte.Domain.Common;

// A1: Classe base que demonstra os pilares de OO — Abstração (modela a noção genérica de "entidade"),
//     Herança (serve de base para Customer e demais entidades) e Polimorfismo (sobrescreve Equals/GetHashCode
//     e os operadores ==/!=). O Encapsulamento aparece nos setters protegidos das propriedades.
// B1: Uso correto de modificadores de acesso (public/protected), propriedades com setters protegidos,
//     métodos públicos (Deactivate/Activate/MarkAsUpdated) e construtor protegido que inicializa o estado.
// C1: Define uma hierarquia flexível e extensível: a igualdade por identidade (Id) é herdada por todas
//     as entidades; subclasses apenas reaproveitam o comportamento sem reimplementá-lo.
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
