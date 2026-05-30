namespace ShopFlexByte.Domain.Entities;

public sealed class Category(string name)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; } = name;
}
