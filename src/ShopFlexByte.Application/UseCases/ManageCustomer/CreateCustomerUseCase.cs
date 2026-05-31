using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Domain.ValueObjects;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

// L3: Padrão Controller (GRASP) — o use case coordena a operação de criação (constrói os Value Objects,
//     invoca a factory do aggregate e persiste via repositório), separando a lógica de controle/orquestração
//     da lógica de domínio (que permanece em Customer/Address/etc.).
// I3/J3: Single Responsibility — a classe tem uma única razão para mudar: o fluxo de criação de cliente.
// K3: Low Coupling — depende apenas da abstração ICustomerRepository, injetada via construtor (DIP).
public sealed class CreateCustomerUseCase(ICustomerRepository customerRepository) : ICreateCustomerUseCase
{
    public async Task<Result<CustomerCreatedResult>> CreateCustomerAsync(CreateCustomerCommand command)
    {
        if (command is null) 
            return Result.Failure<CustomerCreatedResult>("Customer cannot be null.");
        
        var errors = new List<string>();

        var primaryAddressResult = Address.Create(
            command.PrimaryAddress.Street,
            command.PrimaryAddress.Number,
            command.PrimaryAddress.Neighborhood,
            command.PrimaryAddress.City,
            command.PrimaryAddress.State,
            command.PrimaryAddress.ZipCode);

        if (primaryAddressResult.IsFailure)
            errors.Add(primaryAddressResult.Error);

        var fullNameResult = FullName.Create(command.FullName);

        if (fullNameResult.IsFailure)
            errors.Add(fullNameResult.Error);

        var cpfResult = Cpf.Create(command.Cpf);
        
        if (cpfResult.IsFailure) 
            errors.Add(cpfResult.Error);

        var emailResult = Email.Create(command.Email);
        
        if (emailResult.IsFailure) 
            errors.Add(emailResult.Error);

        if (errors.Count > 0)
            return Result.Failure<CustomerCreatedResult>(string.Join("; ", errors));
    
        var customerResult = Customer.Create(
            fullNameResult.Value, 
            cpfResult.Value, 
            emailResult.Value, 
            primaryAddressResult.Value);
        
        if (customerResult.IsFailure)
            return Result.Failure<CustomerCreatedResult>(customerResult.Error);

        var createdCustomer = await customerRepository.CreateAsync(customerResult.Value);        

        return Result.Success(CustomerMapper.ToResult(createdCustomer));
    }    
}
