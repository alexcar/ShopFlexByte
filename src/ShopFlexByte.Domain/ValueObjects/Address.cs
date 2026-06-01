using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Domain.ValueObjects;

// E2: Value Object — sem identidade própria, definido por seus atributos e imutável; a igualdade é
//     estrutural (via GetEqualityComponents herdado de ValueObject). Conceito central do DDD.
// G2: O método estático Create atua como Factory que valida e constrói o VO, mantendo o construtor privado.
public sealed class Address : ValueObject
{
    public string Street { get; }
    public string Number { get; }
    public string Neighborhood { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }

    private Address(string street, string number, string neighborhood, string city, string state, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public static Result<Address> Create(string street, string number, string neighborhood, string city, string state, string zipCode)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(street))
        {
            errors.Add("É necessário ter acesso à rua.");
        }

        if (string.IsNullOrWhiteSpace(number))
        {
            errors.Add("É necessário um número.");
        }

        if (string.IsNullOrWhiteSpace(neighborhood))
        {
            errors.Add("É necessário ter um bairro.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            errors.Add("É necessário informar a cidade.");
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            errors.Add("É necessário informar o estado.");
        }

        if (string.IsNullOrWhiteSpace(zipCode))
        {
            errors.Add("É necessário informar o código postal.");
        }

        if (errors.Any())
        {
            return Result.Failure<Address>(string.Join("; ", errors));
        }
        
        return Result.Success(new Address(street, number, neighborhood, city, state, zipCode));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return Number;
        yield return Neighborhood;
        yield return City;
        yield return State;
        yield return ZipCode;
    }

    public override string ToString() =>
        $"{Street} {Number}, {Neighborhood}, {City}, {State} {ZipCode}";
}
