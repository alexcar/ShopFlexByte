namespace ShopFlexByte.Application.UseCases.RemoveItemFromCart;

public sealed class RemoveItemFromCartInput(Guid userId, Guid productId, int quantity)
{
    public Guid UserId { get; set; } = userId;
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; } = quantity;
}
