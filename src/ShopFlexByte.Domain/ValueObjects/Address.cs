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

        var address = new Address(street, number, neighborhood, city, state, zipCode);
        
        return Result.Success(address);
    }

    // Value Object equality
    private bool Equals(Address other)
    {
        return Street == other.Street &&
               Number == other.Number &&
               Neighborhood == other.Neighborhood &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is Address other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, Number, Neighborhood, City, State, ZipCode);
    }

    public static bool operator ==(Address left, Address right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Address left, Address right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return $"{Street} {Number}, {Neighborhood}, {City}, {State} {ZipCode}";
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
}
