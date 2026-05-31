using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Infrastructure.Persistence.Entities;

public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockLevel { get; set; }
    public Guid CategoryId { get; set; }
}
