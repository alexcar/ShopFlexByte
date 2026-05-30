using ShopFlexByte.Application.UseCases.AddCustomer;
using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface ICreateCustomerUseCase
{
    Task<Result<CustomerDto>> CreateCustomerAsync(CustomerDto request);
}
