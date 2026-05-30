using ShopFlexByte.Application.UseCases.ManageCustomer;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface ISetPrimaryAddress
{
    Task<Result> SetPrimaryAddressAsync(Guid customerId, CreateAddressCommand command);
}
