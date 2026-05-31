using ShopFlexByte.Domain.Common;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IDeleteCustomerUseCase
{
    Task<Result> DeleteCustomerAsync(Guid customerId);
}
