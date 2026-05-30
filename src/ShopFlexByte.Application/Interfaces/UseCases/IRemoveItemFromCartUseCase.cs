using ShopFlexByte.Application.UseCases.RemoveItemFromCart;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IRemoveItemFromCartUseCase
{
    Task RemoveItemFromCartAsync(RemoveItemFromCartInput input);
}
