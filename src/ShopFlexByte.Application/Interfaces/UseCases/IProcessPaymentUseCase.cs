using ShopFlexByte.Application.UseCases.ProcessPayment;
using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface IProcessPaymentUseCase
{
    Task<Order> ProcessPaymentAsync(ProcessPaymentInput input);
}
