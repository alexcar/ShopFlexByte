namespace ShopFlexByte.Domain.Entities;

public sealed class Address(string street, string number, string city, string state, string zipCode)
{
    public string Street { get; } = street;
    public string Number { get; } = number;
    public string City { get; } = city;
    public string State { get; } = state;
    public string ZipCode { get; } = zipCode;   

    //public Address(string street, string number, string city, string state, string zipCode)
    //{
    //    if (string.IsNullOrWhiteSpace(street))
    //        throw new ArgumentException("Street cannot be null or empty.", nameof(street));
    //    if (string.IsNullOrWhiteSpace(number))
    //        throw new ArgumentException("Number cannot be null or empty.", nameof(number));
    //    if (string.IsNullOrWhiteSpace(city))
    //        throw new ArgumentException("City cannot be null or empty.", nameof(city));
    //    if (string.IsNullOrWhiteSpace(state))
    //        throw new ArgumentException("State cannot be null or empty.", nameof(state));
    //    if (string.IsNullOrWhiteSpace(zipCode))
    //        throw new ArgumentException("ZipCode cannot be null or empty.", nameof(zipCode));

    //    Street = street;
    //    Number = number;
    //    City = city;
    //    State = state;
    //    ZipCode = zipCode;
    //}

    // Value Object equality
    private bool Equals(Address other)
    {
        return Street == other.Street &&
               Number == other.Number &&
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
        return HashCode.Combine(Street, Number, City, State, ZipCode);
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
        return $"{Street} {Number}, {City}, {State} {ZipCode}";
    }
}
