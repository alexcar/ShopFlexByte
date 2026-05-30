using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.Data;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task UpdateAsync(Product product);
}
