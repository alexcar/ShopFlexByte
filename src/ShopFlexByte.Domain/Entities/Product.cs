namespace ShopFlexByte.Domain.Entities;

// B1: Uso de primary constructor e propriedades com setters privados/get-only; o estoque só muda por método.
// D1: Encapsulamento — StockLevel tem set privado e a única forma de alterá-lo é UpdateStockLevel, que
//     protege a invariante (estoque não negativo), ocultando os detalhes de validação do chamador.
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
            throw new ArgumentException("O nível de estoque não pode ser negativo.", nameof(stockLevel));
        }

        StockLevel = stockLevel;
    }
}
