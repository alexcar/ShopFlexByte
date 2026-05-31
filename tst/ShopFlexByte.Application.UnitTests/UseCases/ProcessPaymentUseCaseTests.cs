using AutoMapper;
using NSubstitute;
using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.Services.Payment;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Application.UseCases.CalculateCartTotal;
using ShopFlexByte.Application.UseCases.ProcessPayment;
using ShopFlexByte.Application.Mapping;
using ShopFlexByte.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using ShopFlexByte.Domain.Enums;

namespace ShopFlexByte.Application.UnitTests.UseCases;

// N4: Cobre métodos com regra de negócio relevante — valida que, conforme o status retornado pelo gateway,
//     o pedido passa para Paid (pagamento aprovado) ou PaymentFailed (pagamento recusado) e que o carrinho é limpo.
// O4: Mocks/stubs com NSubstitute para o gateway de pagamento e repositórios, isolando a unidade sob teste.
public class ProcessPaymentUseCaseTests
{
    private readonly IMapper _mapper;

    public ProcessPaymentUseCaseTests()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApplicationMappingProfile>();
        }, NullLoggerFactory.Instance).CreateMapper();
    }

    [Fact]
    public async Task ProcessPaymentAsync_PaymentSuccessful_UpdatesOrderStatusToPaid()
    {
        var mockOrderRepository = Substitute.For<IOrderRepository>();
        mockOrderRepository.CreateOrderAsync(Arg.Any<Order>())
            .Returns(info => info.Arg<Order>()); // Return the order passed to it

        var mockShoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        mockShoppingCartRepository.GetByUserIdAsync(Arg.Any<Guid>())
            .Returns(new ShoppingCart(Guid.NewGuid()));

        var mockPaymentGateway = Substitute.For<IPaymentGateway>();
        mockPaymentGateway.ProcessPaymentAsync(Arg.Any<PaymentRequest>())
            .Returns(new PaymentResult { Status = PaymentStatus.Success });

        var mockCalculateCartTotalUseCase = Substitute.For<ICalculateCartTotalUseCase>();
        mockCalculateCartTotalUseCase.CalculateTotalAsync(Arg.Any<CalculateCartTotalInput>())
            .Returns(100.0m);

        var useCase = new ProcessPaymentUseCase(
            mockOrderRepository,
            mockPaymentGateway,
            mockShoppingCartRepository,
            mockCalculateCartTotalUseCase,
            _mapper);

        var input = new ProcessPaymentInput
        {
            UserId = Guid.NewGuid(),
            Items = new List<ShoppingCartItem>(),
            CardNumber = "1234567890123456",
            CardHolderName = "John Doe",
            ExpirationMonthYear = "12/23",
            CVV = "123",
            PostalCode = "12345"
        };

        await useCase.ProcessPaymentAsync(input);

        await mockShoppingCartRepository.Received(1).DeleteByUserIdAsync(Arg.Any<Guid>());
        await mockOrderRepository.Received(1).UpdateOrderAsync(Arg.Is<Order>(o => o.Status == OrderStatus.Paid));
    }

    [Fact]
    public async Task ProcessPaymentAsync_PaymentFailed_UpdatesOrderStatusToPaymentFailed()
    {
        var mockOrderRepository = Substitute.For<IOrderRepository>();
        mockOrderRepository.CreateOrderAsync(Arg.Any<Order>())
            .Returns(info => info.Arg<Order>()); // Return the order passed to it

        var mockShoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        mockShoppingCartRepository.GetByUserIdAsync(Arg.Any<Guid>())
            .Returns(new ShoppingCart(Guid.NewGuid()));

        var mockPaymentGateway = Substitute.For<IPaymentGateway>();
        mockPaymentGateway.ProcessPaymentAsync(Arg.Any<PaymentRequest>())
            .Returns(new PaymentResult { Status = PaymentStatus.Failed });

        var mockCalculateCartTotalUseCase = Substitute.For<ICalculateCartTotalUseCase>();
        mockCalculateCartTotalUseCase.CalculateTotalAsync(Arg.Any<CalculateCartTotalInput>())
            .Returns(100.0m);

        var useCase = new ProcessPaymentUseCase(
            mockOrderRepository,
            mockPaymentGateway,
            mockShoppingCartRepository,
            mockCalculateCartTotalUseCase,
            _mapper);

        var input = new ProcessPaymentInput
        {
            UserId = Guid.NewGuid(),
            Items = new List<ShoppingCartItem>(),
            CardNumber = "1234567890123456",
            CardHolderName = "John Doe",
            ExpirationMonthYear = "12/23",
            CVV = "123",
            PostalCode = "12345"
        };

        await useCase.ProcessPaymentAsync(input);

        await mockShoppingCartRepository.Received(1).DeleteByUserIdAsync(Arg.Any<Guid>());
        await mockOrderRepository.Received(1).UpdateOrderAsync(Arg.Is<Order>(o => o.Status == OrderStatus.PaymentFailed));
    }
}
