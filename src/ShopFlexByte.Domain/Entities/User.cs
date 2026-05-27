namespace ShopFlexByte.Domain.Entities;

public sealed class User(string username, string email, string fullName, Address primaryAddress)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; } = username;
    public string Email { get; } = email;
    public string FullName { get; } = fullName;
    public Address PrimaryAddress { get; private set; } = primaryAddress;
    private readonly List<Address> _otherAddresses = new();
    public IReadOnlyCollection<Address> OtherAddresses => _otherAddresses.AsReadOnly();

    //public User(string username, string email, string fullName, Address primaryAddress)
    //{
    //    Username = username;
    //    Email = email;
    //    FullName = fullName;
    //    PrimaryAddress = primaryAddress;
    //}

    public void SetPrimaryAddress(Address address)
    {
        PrimaryAddress = address;
    }

    public void AddOtherAddress(Address address)
    {
        _otherAddresses.Add(address);
    }

    public void RemoveOtherAddress(Address address)
    {
        _otherAddresses.Remove(address);
    }
}
