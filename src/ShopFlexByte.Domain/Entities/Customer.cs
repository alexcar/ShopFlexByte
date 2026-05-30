using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.ValueObjects;


namespace ShopFlexByte.Domain.Entities;

public sealed class Customer() : Entity
{
    public FullName FullName { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Email Email { get; private set; } = null!;    
    public Address PrimaryAddress { get; private set; } = null!;

    private readonly List<Address> _otherAddresses = new();
    public IReadOnlyCollection<Address> OtherAddresses => _otherAddresses.AsReadOnly();    

    public static Result<Customer> Create(FullName fullName, Cpf cpf, Email email, Address primaryAddress)
    {
        var errors = new List<string>();
        
        if (fullName is null)
        {
            errors.Add("Full name is required.");
        }        

        if (cpf is null)
        {
            errors.Add("CPF is required.");
        }

        if (email is null)
        {
            errors.Add("Email is required.");
        }        

        if (primaryAddress is null)
        {
            errors.Add("Primary address is required.");
        }

        if (errors.Any())
        {
            return Result.Failure<Customer>(string.Join("; ", errors));
        }

        var customer = new Customer
        {
            FullName = fullName!,
            Cpf = cpf!,
            Email = email!,
            PrimaryAddress = primaryAddress!,
        };

        return Result.Success(customer);
    }

    public Result<Address> SetPrimaryAddress(Address address)
    {
        if (address is null)
        {
            return Result.Failure<Address>("Primary address is required.");
        }

        // Se o endereço já estiver na lista de outros endereços, removê-lo
        if (_otherAddresses.Contains(address))
        {
            _otherAddresses.Remove(address);
        }

        // Se o endereço atual for diferente do novo endereço, adicionar o endereço atual à lista de outros endereços
        if (PrimaryAddress is not null && !PrimaryAddress.Equals(address))
        {
            _otherAddresses.Add(PrimaryAddress);
        }

        // Defini o novo endereço como principal        
        PrimaryAddress = address;

        return Result.Success(address);
    }

    public Result<Address> AddOtherAddress(Address address)
    {
        if (address is null)
        {
            return Result.Failure<Address>("Address is required.");
        }

        // Verifica se o endereço já existe na lista de outros endereços ou é o endereço principal
        if (_otherAddresses.Contains(address) || (PrimaryAddress is not null && PrimaryAddress.Equals(address)))
        {
            return Result.Failure<Address>("Endereço já existe.");
        }

        _otherAddresses.Add(address);

        return Result.Success(address);
    }

    public Result RemoveOtherAddress(Address address)
    {
        if (!_otherAddresses.Contains(address))
        {
            return Result.Failure("Endereço não encontrado.");
        }

        _otherAddresses.Remove(address);
        
        return Result.Success();
    }    
}
