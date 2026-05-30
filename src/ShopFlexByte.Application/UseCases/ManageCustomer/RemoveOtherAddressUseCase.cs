using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class RemoveOtherAddressUseCase(ICustomerRepository customerRepository) : IRemoveOtherAddress
{
    public async Task<Result> RemoveOtherAddressAsync(Guid customerId, CreateAddressCommand command)
    {
        if (command is null)
            return Result.Failure("Command is null.");
        
        var customer = await customerRepository.GetByIdAsync(customerId);
        
        if (customer is null)
            return Result.Failure("Customer not found.");        

        var addressToRemove = customer.OtherAddresses.FirstOrDefault(a =>
            a.Street == command.Street &&
            a.Number == command.Number &&
            a.Neighborhood == command.Neighborhood &&
            a.City == command.City &&
            a.State == command.State &&
            a.ZipCode == command.ZipCode);

        if (addressToRemove is null)
            return Result.Failure("Endereço não encontrado.");

        var removeOtherAddressResult = customer.RemoveOtherAddress(addressToRemove);
        
        if (removeOtherAddressResult.IsFailure)
        {
            return Result.Failure(removeOtherAddressResult.Error);
        }
        
        await customerRepository.UpdateAsync(customer);
        
        return Result.Success();
    }
}
