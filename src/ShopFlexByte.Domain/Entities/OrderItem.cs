namespace ShopFlexByte.Domain.Entities;

public sealed class OrderItem(Guid productId, string productName, decimal productPrice, int quantity)
{
    public Guid ProductId { get; } = productId;
    public string ProductName { get; } = productName;
    public decimal ProductPrice { get; } = productPrice;
    public int Quantity { get; } = quantity;
}
