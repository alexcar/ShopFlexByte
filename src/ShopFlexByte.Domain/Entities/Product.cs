namespace ShopFlexByte.Domain.Entities;

public sealed class Product(string name, decimal price, int stockLevel, Guid categoryId)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; } = name;
    public decimal Price { get; } = price;
    public int StockLevel { get; private set; } = stockLevel;
    public Guid CategoryId { get; } = categoryId;    

    public void UpdateStockLevel(int stockLevel)
    {
        if (stockLevel < 0)
        {
            throw new ArgumentException("Stock level cannot be negative", nameof(stockLevel));
        }

        StockLevel = stockLevel;
    }
}
