using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Domain.ValueObjects;

// E2: Value Object que modela "Nome completo" decompondo-o em FirstName/LastName, imutável e validado.
// G2: Create funciona como Factory do VO (separa a construção/validação do estado interno).
public sealed class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string fullName)
    {
        var normalized = fullName?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalized))
            return Result.Failure<FullName>("Full name is required.");

        var names = normalized.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        
        if (names.Length < 2)
            return Result.Failure<FullName>("Full name must include both first and last name.");

        return Result.Success(new FullName(names[0], names[1]));
    }

    protected override IEnumerable<object> GetEqualityComponents() { yield return FirstName; yield return LastName; }
    public override string ToString() => $"{FirstName} {LastName}";
}
