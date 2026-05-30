using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.UseCases.RemoveItemFromCart;

public sealed class RemoveItemFromCartUseCase(IShoppingCartRepository shoppingCartRepository) : IRemoveItemFromCartUseCase
{
    public async Task RemoveItemFromCartAsync(RemoveItemFromCartInput input)
    {
        ShoppingCart? cart = await shoppingCartRepository.GetByUserIdAsync(input.UserId);

        if (cart != null)
        {
            cart.RemoveItem(input.ProductId, input.Quantity);

            await shoppingCartRepository.SaveAsync(cart);

            if (!cart.Items.Any())
            {
                await shoppingCartRepository.DeleteByUserIdAsync(input.UserId);
            }
        }
    }
}
