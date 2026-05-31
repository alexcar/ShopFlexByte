using NSubstitute;
using Refit;
using ShopFlexByte.Application.Interfaces.Services.Payment;
using ShopFlexByte.Infrastructure.Clients;
using ShopFlexByte.Infrastructure.Services;
using System.Net;

namespace ShopFlexByte.Infrastructure.UnitTests.Services;

public class PaymentGatewayTests
{
    [Fact]
    public async Task ProcessPaymentAsync_ReturnsPaymentResult()
    {
        var mockApi = Substitute.For<IPaymentGatewayApi>();
        var mockApiResponse = new ApiResponse<string>(new HttpResponseMessage(HttpStatusCode.Created), string.Empty, new RefitSettings());
        mockApi.ProcessPaymentAsync(Arg.Any<object>()).Returns(mockApiResponse);
        var paymentRequest = new PaymentRequest();
        var paymentGateway = new PaymentGateway(mockApi);

        var result = await paymentGateway.ProcessPaymentAsync(paymentRequest);

        Assert.NotNull(result);
        Assert.NotEmpty(result.TransactionId);
        Assert.Equal(PaymentStatus.Success, result.Status);
        Assert.True(result.Timestamp <= DateTime.UtcNow);
        await mockApi.Received(1).ProcessPaymentAsync(Arg.Any<object>());
    }

    [Fact]
    public async Task GetPaymentStatusAsync_ReturnsPaymentStatus()
    {
        var mockApi = Substitute.For<IPaymentGatewayApi>();
        var mockApiResponse = new ApiResponse<string>(new HttpResponseMessage(HttpStatusCode.OK), string.Empty, new RefitSettings());
        mockApi.GetPaymentStatusAsync(Arg.Any<string>()).Returns(mockApiResponse);
        var paymentId = "paymentId";
        var paymentGateway = new PaymentGateway(mockApi);

        var result = await paymentGateway.GetPaymentStatusAsync(paymentId);

        Assert.Equal(PaymentStatus.Success, result);
        await mockApi.Received(1).GetPaymentStatusAsync(Arg.Any<string>());
    }
}
