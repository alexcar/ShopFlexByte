using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Application.UseCases.AddCustomer;
using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Domain.ValueObjects;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class CreateCustomerUseCase(ICustomerRepository customerRepository) : ICreateCustomerUseCase
{
    public async Task<Result<CustomerDto>> CreateCustomerAsync(CustomerDto request)
    {
        var errors = new List<string>();

        if (request is null) 
        {
            throw new ArgumentNullException(nameof(request));
        }        

        var primaryAddressResult = Address.Create(
            request.PrimaryAddress.Street,
            request.PrimaryAddress.Number,
            request.PrimaryAddress.Neighborhood,
            request.PrimaryAddress.City,
            request.PrimaryAddress.State,
            request.PrimaryAddress.ZipCode);

        if (primaryAddressResult.IsFailure)
        {
            errors.AddRange(primaryAddressResult.Error);
        }

        var fullNameResult = FullName.Create(request.FullName);

        if (fullNameResult.IsFailure)
        {
            errors.AddRange(fullNameResult.Error);
        }

        var cpfResult = Cpf.Create(request.Cpf);
        
        if (cpfResult.IsFailure) 
        {
            errors.AddRange(cpfResult.Error);
        }

        var emailResult = Email.Create(request.Email);
        
        if (emailResult.IsFailure) 
        {
            errors.AddRange(emailResult.Error);
        }

        if (errors.Any())
        {
            var combinedErrors = string.Join("; ", errors);
            
            return Result.Failure<CustomerDto>(combinedErrors);
        }

        var customerResult = Customer.Create(fullNameResult.Value, cpfResult.Value, emailResult.Value, primaryAddressResult.Value);
        
        if (customerResult.IsFailure)
        {
            return Result.Failure<CustomerDto>(customerResult.Error);
        }

        var createdCustomer = await customerRepository.CreateAsync(customerResult.Value);
        var response = MapToCustomerDto(createdCustomer);

        return Result.Success(response);
    }

    private CustomerDto MapToCustomerDto(Customer createdCustomer)
    {
        return new CustomerDto
        {
            FullName = createdCustomer.FullName.ToString(),
            Email = createdCustomer.Email.Value,
            PrimaryAddress = new AddressDto
            {
                Street = createdCustomer.PrimaryAddress.Street,
                Number = createdCustomer.PrimaryAddress.Number,
                Neighborhood = createdCustomer.PrimaryAddress.Neighborhood,
                City = createdCustomer.PrimaryAddress.City,
                State = createdCustomer.PrimaryAddress.State,
                ZipCode = createdCustomer.PrimaryAddress.ZipCode
            },
            OtherAddresses = createdCustomer.OtherAddresses.Select(a => MapToAddressDto(a)).ToList()
        };
    }

    private AddressDto MapToAddressDto(Address address)
    {
        return new AddressDto
        {
            Street = address.Street,
            Number = address.Number,
            Neighborhood = address.Neighborhood,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
    }

    
}
