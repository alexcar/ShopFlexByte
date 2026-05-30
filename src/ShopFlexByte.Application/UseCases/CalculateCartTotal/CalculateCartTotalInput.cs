namespace ShopFlexByte.Application.UseCases.CalculateCartTotal;

public sealed class CalculateCartTotalInput(Guid userId)
{
    public Guid UserId { get; } = userId;
}
