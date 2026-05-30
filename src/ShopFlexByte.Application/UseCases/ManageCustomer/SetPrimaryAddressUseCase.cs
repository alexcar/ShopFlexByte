using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.ValueObjects;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class SetPrimaryAddressUseCase(ICustomerRepository customerRepository) : ISetPrimaryAddress
{
    public async Task<Result> SetPrimaryAddressAsync(Guid customerId, CreateAddressCommand command)
    {
        if (command is null)
            return Result.Failure("Command is null.");        

        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
            return Result.Failure("Customer not found.");

        var addressResult = Address.Create(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.ZipCode);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        var setPrimaryAddressResult = customer.SetPrimaryAddress(addressResult.Value);

        if (setPrimaryAddressResult.IsFailure)
        {
            return Result.Failure(setPrimaryAddressResult.Error);
        }

        await customerRepository.UpdateAsync(customer);

        return Result.Success();
    }
}
