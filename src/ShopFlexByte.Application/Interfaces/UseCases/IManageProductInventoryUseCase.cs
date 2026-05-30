namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IManageProductInventoryUseCase
{
    Task UpdateProductInventoryAsync(Guid userId, Guid productId, int stockLevel);    
}
