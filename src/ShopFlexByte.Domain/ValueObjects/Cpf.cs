using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Domain.ValueObjects;

// E2: Value Object que encapsula a regra de negócio do CPF (Ubiquitous Language do domínio brasileiro).
// D1: Abstração/Encapsulamento — toda a complexidade do algoritmo de validação dos dígitos verificadores
//     fica oculta no método privado Validar; o exterior só vê Create (entrada) e Value/ToString (saída).
public sealed class Cpf : ValueObject
{
    public string Value { get; }
    private Cpf(string value) => Value = value;

    public static Result<Cpf> Create(string cpf)
    {
        var numeros = new string(cpf.Where(char.IsDigit).ToArray());

        if (numeros.Length != 11 || !Validar(numeros))
            return Result.Failure<Cpf>("CPF inválido.");

        return Result.Success(new Cpf(numeros));
    }

    private static bool Validar(string cpf)
    {
        if (cpf.Distinct().Count() == 1) return false;

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var soma1 = cpf.Take(9).Select((c, i) => (c - '0') * multiplicadores1[i]).Sum();
        var dig1 = soma1 % 11 < 2 ? 0 : 11 - soma1 % 11;
        var soma2 = cpf.Take(10).Select((c, i) => (c - '0') * multiplicadores2[i]).Sum();
        var dig2 = soma2 % 11 < 2 ? 0 : 11 - soma2 % 11;

        return cpf[9] - '0' == dig1 && cpf[10] - '0' == dig2;
    }

    protected override IEnumerable<object> GetEqualityComponents() { yield return Value; }
    public override string ToString() =>
        $"{Value[..3]}.{Value[3..6]}.{Value[6..9]}-{Value[9..11]}";
}
