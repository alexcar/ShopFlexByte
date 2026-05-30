using ShopFlexByte.Application.UseCases.ManageCustomer;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IUpdateCustomerUserCase
{
    Task<Result> UpdateCustomerAsync(Guid customerId, UpdateCustomerCommand command);
}
