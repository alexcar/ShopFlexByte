using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.UseCases.ProcessPayment;

public sealed class ProcessPaymentInput
{
    public Guid UserId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
    public string ExpirationMonthYear { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
