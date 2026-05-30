using ShopFlexByte.Application.UseCases.ManageCustomer;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IRemoveOtherAddress
{
    Task<Result> RemoveOtherAddressAsync(Guid customerId, CreateAddressCommand command);
}
