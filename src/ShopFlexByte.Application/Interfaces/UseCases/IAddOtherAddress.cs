using ShopFlexByte.Application.UseCases.ManageCustomer;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IAddOtherAddress
{
    Task<Result> AddOtherAddressAsync(Guid customerId, CreateAddressCommand command);
}
