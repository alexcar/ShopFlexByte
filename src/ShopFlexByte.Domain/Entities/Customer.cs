using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.ValueObjects;


namespace ShopFlexByte.Domain.Entities;

// E2: Entity do domínio (Ubiquitous Language: "Cliente") com identidade própria herdada de Entity,
//     composta por Value Objects (FullName, Cpf, Email, Address) — modelagem coerente com DDD.
// F2: Aggregate Root do Bounded Context de clientes — controla a consistência da coleção de endereços
//     (PrimaryAddress + OtherAddresses), que só pode ser alterada por meio de seus métodos.
// C1: Herda de Entity reaproveitando identidade, igualdade e soft delete (Deactivate).
public sealed class Customer : Entity
{
    // B1: Construtor privado — impede instanciação direta fora do factory method (encapsula a criação).
    // O EF Core consegue usar construtores privados sem parâmetros normalmente.
    private Customer() { }

    public FullName FullName { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Email Email { get; private set; } = null!;    
    public Address PrimaryAddress { get; private set; } = null!;

    // D1: Encapsulamento — a lista é privada e mutável; expõe-se apenas uma visão somente-leitura,
    //     impedindo que código externo altere a coleção sem passar pelas regras do aggregate.
    private readonly List<Address> _otherAddresses = new();
    public IReadOnlyCollection<Address> OtherAddresses => _otherAddresses.AsReadOnly();

    // G2: Factory Method — responsável por CRIAR um Customer válido, validando invariantes e devolvendo
    //     Result em vez de lançar exceção. Difere de um Domain Service (que coordenaria regras entre
    //     múltiplos aggregates): aqui a responsabilidade é apenas a construção consistente da própria entidade.
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

        if (errors.Count > 0)
            return Result.Failure<Customer>(string.Join("; ", errors));

        var customer = new Customer
        {
            FullName = fullName!,
            Cpf = cpf!,
            Email = email!,
            PrimaryAddress = primaryAddress!,
        };

        return Result.Success(customer);
    }

    public Result Update(FullName fullName, Email email)
    {
        var errors = new List<string>();

        if (fullName is null)
        {
            errors.Add("Full name is required.");
        }
        
        if (email is null)
        {
            errors.Add("Email is required.");
        }
        if (errors.Count > 0)
        {
            return Result.Failure(string.Join("; ", errors));
        }
        
        FullName = fullName!;
        Email = email!;
        
        return Result.Success();
    }

    public Result Delete()
    {
        Deactivate();
        
        return Result.Success();
    }

    public Result<Address> SetPrimaryAddress(Address address)
    {
        if (address is null)
            return Result.Failure<Address>("Primary address is required.");

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
        if (address is null)
        {
            return Result.Failure<Address>("Address is required.");
        }        

        if (!_otherAddresses.Remove(address))
            return Result.Failure("Endereço não encontrado na lista.");

        return Result.Success();
    }    
}
