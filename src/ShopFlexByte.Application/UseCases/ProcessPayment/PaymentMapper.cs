using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.UseCases.ProcessPayment;

internal static class PaymentMapper
{
    internal static List<OrderItem> ToOrderItems(List<ShoppingCartItem> items) =>
        items.Select(ToOrderItem).ToList();

    internal static OrderItem ToOrderItem(ShoppingCartItem item) =>
        new(item.ProductId, item.ProductName, item.ProductPrice, item.Quantity);
}
