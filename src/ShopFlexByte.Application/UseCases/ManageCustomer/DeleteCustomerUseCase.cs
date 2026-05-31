using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class DeleteCustomerUseCase(ICustomerRepository customerRepository) : IDeleteCustomerUseCase
{
    public async Task<Result> DeleteCustomerAsync(Guid customerId)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
            return Result.Failure("Customer not found.");

        customer.Delete();

        await customerRepository.DeleteAsync(customer.Id);

        return Result.Success();
    }
}
