using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.ValueObjects;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class AddOtherAddressUseCase(ICustomerRepository customerRepository) : IAddOtherAddress
{
    public async Task<Result> AddOtherAddressAsync(Guid customerId, CreateAddressCommand command)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return Result.Failure("Cliente não encontrado.");
        }

        if (command is null)
        {
            return Result.Failure("O endereço não foi informado.");
        }        

        var otherAddressResult = Address.Create(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.ZipCode);

        if (otherAddressResult.IsFailure)
        {
            return Result.Failure(otherAddressResult.Error);
        }                

        var addOtherAddressResult = customer.AddOtherAddress(otherAddressResult.Value);

        if (addOtherAddressResult.IsFailure)
        {
            return Result.Failure(addOtherAddressResult.Error);
        }

        await customerRepository.UpdateAsync(customer);

        return Result.Success();
    }
}
