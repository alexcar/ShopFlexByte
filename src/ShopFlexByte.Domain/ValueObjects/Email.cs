using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Domain.ValueObjects;

// E2: Value Object de e-mail — imutável, normalizado e validado na criação; garante que toda instância
//     existente é, por construção, um e-mail válido (invariante do domínio).
// D1: Detalhe de validação encapsulado no método privado Validar, expondo apenas a interface Create.
public sealed class Email : ValueObject
{
    public string Value { get; }
    private Email(string value) => Value = value;

    public static Result<Email> Create(string email)
    {
        var normalizado = email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (!Validar(normalizado))
            return Result.Failure<Email>("E-mail inválido.");

        return Result.Success(new Email(normalizado));
    }

    private static bool Validar(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length > 254)
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents() { yield return Value; }
    public override string ToString() => Value;
}
