using Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.Services.Payment;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Application.UseCases.CalculateCartTotal;
using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Domain.Enums;

namespace ShopFlexByte.Application.UseCases.ProcessPayment;

public sealed class ProcessPaymentUseCase(
    IOrderRepository orderRepository,
    IPaymentGateway paymentGateway,
    IShoppingCartRepository shoppingCartRepository,
    ICalculateCartTotalUseCase calculateCartTotalUseCase,
    IMapper mapper)
    : IProcessPaymentUseCase
{
    public async Task<Order> ProcessPaymentAsync(ProcessPaymentInput input)
    {
        var calculateTotalInput = new CalculateCartTotalInput(input.UserId);

        decimal totalAmount = await calculateCartTotalUseCase.CalculateTotalAsync(calculateTotalInput);

        var orderItems = mapper.Map<List<OrderItem>>(input.Items);
        var order = new Order(input.UserId, orderItems, totalAmount);
        order = await orderRepository.CreateOrderAsync(order);

        await shoppingCartRepository.DeleteByUserIdAsync(input.UserId);

        var paymentRequest = new PaymentRequest
        {
            Amount = totalAmount,
            CardNumber = input.CardNumber,
            CardHolderName = input.CardHolderName,
            ExpirationMonthYear = input.ExpirationMonthYear,
            CVV = input.CVV,
            PostalCode = input.PostalCode
        };

        var paymentResult = await paymentGateway.ProcessPaymentAsync(paymentRequest);

        switch (paymentResult.Status)
        {
            case PaymentStatus.Success:
                order.Status = OrderStatus.Paid;
                await orderRepository.UpdateOrderAsync(order);
                break;
            case PaymentStatus.Pending:
                // Payment is pending, you can log a message or notify the user.
                break;
            case PaymentStatus.Failed:
                order.Status = OrderStatus.PaymentFailed;
                await orderRepository.UpdateOrderAsync(order);
                break;
            default:
#pragma warning disable S3928 // Parameter names used into ArgumentException constructors should match an existing one 
                throw new ArgumentOutOfRangeException(nameof(paymentResult.Status));
#pragma warning restore S3928 // Parameter names used into ArgumentException constructors should match an existing one 
        }

        return order;
    }
}
