using NSubstitute;
using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.UseCases.AddItemToCart;
using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.UnitTests.UseCases;

// M4: Aplica os princípios de testes unitários — Isolamento (dependências substituídas por mocks NSubstitute),
//     Repetibilidade (sem estado externo/banco, dados gerados no próprio teste), Rapidez (tudo em memória),
//     Auto-verificação (asserts/Received que decidem sozinhos o resultado) e Abrangência (caminho feliz,
//     criação de carrinho inexistente e cenário de exceção por estoque insuficiente).
// O4: Uso adequado de mocks/stubs — GetByIdAsync/GetByUserIdAsync são stubados para retornar dados conhecidos
//     e os repositórios são verificados (Received/DidNotReceive) como mocks de interação.
public class AddItemToCartUseCaseTests
{
    [Fact]
    public async Task AddItemToCartAsync_ValidInput_AddsItemToCart()
    {
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        var productRepository = Substitute.For<IProductRepository>();
        var userId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var shoppingCart = new ShoppingCart(userId);
        var product = new Product("Test Product", 10.00m, 5, categoryId);

        shoppingCartRepository.GetByUserIdAsync(userId).Returns(shoppingCart);
        productRepository.GetByIdAsync(product.Id).Returns(product);

        var useCase = new AddItemToCartUseCase(shoppingCartRepository, productRepository);

        _ = useCase.AddItemToCartAsync(new AddItemToCartInput(userId, product.Id, 1));

        await productRepository.Received(1).GetByIdAsync(product.Id);
        await shoppingCartRepository.Received(1).GetByUserIdAsync(userId);
        await shoppingCartRepository.Received(1).SaveAsync(shoppingCart);
    }

    [Fact]
    public async Task AddItemToCartAsync_ShouldCreateNewCart_WhenCartDoesNotExist()
    {
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        var productRepository = Substitute.For<IProductRepository>();

        var userId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var product = new Product("Test Product", 10.00m, 2, categoryId);

        // Simulate that no cart exists yet
        shoppingCartRepository.GetByUserIdAsync(userId).Returns((ShoppingCart?)null);
        productRepository.GetByIdAsync(product.Id).Returns(product);

        var useCase = new AddItemToCartUseCase(shoppingCartRepository, productRepository);

        await useCase.AddItemToCartAsync(new AddItemToCartInput(userId, product.Id, 2));

        await shoppingCartRepository.Received(1).SaveAsync(Arg.Is<ShoppingCart>(
            cart => cart.UserId == userId &&
                    cart.Items.Any(i => i.ProductId == product.Id && i.Quantity == 2)
        ));
    }

    [Fact]
    public async Task AddItemToCartAsync_ShouldThrow_WhenRequestedQuantityExceedsStock()
    {
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        var productRepository = Substitute.For<IProductRepository>();
        var userId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var product = new Product("Limited Edition Item", 25.00m, stockLevel: 1, categoryId); // Only 1 in stock

        productRepository.GetByIdAsync(product.Id).Returns(product);
        shoppingCartRepository.GetByUserIdAsync(userId).Returns(new ShoppingCart(userId));

        var useCase = new AddItemToCartUseCase(shoppingCartRepository, productRepository);

        var input = new AddItemToCartInput(userId, product.Id, quantity: 2); // Requesting more than available

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.AddItemToCartAsync(input));

        Assert.Contains("Not enough stock", exception.Message);

        await shoppingCartRepository.DidNotReceive().SaveAsync(Arg.Any<ShoppingCart>());
    }
}

