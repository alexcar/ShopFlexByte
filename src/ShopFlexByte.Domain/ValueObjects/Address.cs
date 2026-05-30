using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Domain.ValueObjects;

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
            errors.Add("Street is required.");
        }

        if (string.IsNullOrWhiteSpace(number))
        {
            errors.Add("Number is required.");
        }

        if (string.IsNullOrWhiteSpace(neighborhood))
        {
            errors.Add("Neighborhood is required.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            errors.Add("City is required.");
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            errors.Add("State is required.");
        }

        if (string.IsNullOrWhiteSpace(zipCode))
        {
            errors.Add("Zip code is required.");
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
