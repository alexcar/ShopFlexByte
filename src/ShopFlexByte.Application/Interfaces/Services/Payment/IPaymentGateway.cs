namespace ShopFlexByte.Application.Interfaces.Services.Payment;

// I3/K3: Abstração de serviço externo (gateway de pagamento) declarada na Application e implementada na
//        Infrastructure — aplica o Dependency Inversion Principle e mantém baixo acoplamento com o provedor.
public interface IPaymentGateway
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest paymentRequest);
    Task<PaymentStatus> GetPaymentStatusAsync(string paymentId);
}
