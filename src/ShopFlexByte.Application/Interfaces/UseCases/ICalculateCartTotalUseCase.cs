using ShopFlexByte.Application.UseCases.CalculateCartTotal;

namespace ShopFlexByte.Application.Interfaces.UseCases;

public interface ICalculateCartTotalUseCase
{
    Task<decimal> CalculateTotalAsync(CalculateCartTotalInput input);
}
