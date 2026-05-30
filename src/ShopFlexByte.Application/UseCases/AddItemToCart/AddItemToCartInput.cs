namespace ShopFlexByte.Application.UseCases.AddItemToCart;

public sealed class AddItemToCartInput(Guid userId, Guid productId, int quantity)
{
    public Guid UserId { get; } = userId;
    public Guid ProductId { get; } = productId;
    public int Quantity { get; } = quantity;
}
