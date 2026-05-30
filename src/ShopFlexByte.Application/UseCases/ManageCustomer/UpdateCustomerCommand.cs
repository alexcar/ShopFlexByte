namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed record UpdateCustomerCommand(
    string FullName,
    string Email);
