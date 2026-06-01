using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Domain.Enums;

namespace ShopFlexByte.Application.UseCases.ManageProductInventory;

public sealed class ManageProductInventoryUseCase(
    IUserRepository userRepository,
    IProductRepository productRepository)
    : IManageProductInventoryUseCase
{
    public async Task UpdateProductInventoryAsync(Guid userId, Guid productId, int stockLevel)
    {
        User? user = await userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado.");
        }

        if (!user.Roles.Contains(UserRole.Administrator))
        {
            throw new UnauthorizedAccessException("Usuário não está autorizado a gerenciar o estoque de produtos.");
        }

        Product? product = await productRepository.GetByIdAsync(productId);

        if (product is null)
            return; // Log or handle as needed

        product.UpdateStockLevel(stockLevel);
        await productRepository.UpdateAsync(product);
    }
}
