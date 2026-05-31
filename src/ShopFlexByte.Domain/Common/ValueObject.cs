namespace ShopFlexByte.Domain.Common;

// A1/C1: Classe abstrata base que aplica Herança e Polimorfismo — Address, Cpf, Email e FullName
//        herdam dela e especializam GetEqualityComponents(), permitindo igualdade estrutural polimórfica.
// D1: Aplica Abstração e Encapsulamento — o algoritmo de comparação/hash fica oculto na base e expõe
//     apenas o método abstrato GetEqualityComponents() como contrato claro para as subclasses.
public abstract class ValueObject
{
    // D1: Método abstrato (template) que oculta da subclasse os detalhes de como a igualdade é calculada.
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
