using ShopFlexByte.Application.UseCases.AddItemToCart;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IAddItemToCartUseCase
{
    Task AddItemToCartAsync(AddItemToCartInput input);
}
