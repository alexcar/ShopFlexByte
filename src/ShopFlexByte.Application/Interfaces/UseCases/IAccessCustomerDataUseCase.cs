using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IAccessCustomerDataUseCase
{
    Task<ShoppingCart?> GetCustomerCartAsync(Guid requestingUserId, Guid targetUserId);
    Task<IEnumerable<Order>> GetOrderHistoryAsync(Guid requestingUserId, Guid targetUserId);
}
