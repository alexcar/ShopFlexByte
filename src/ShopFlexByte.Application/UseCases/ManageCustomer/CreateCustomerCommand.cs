namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed record CreateCustomerCommand(
    string FullName,
    string Cpf,
    string Email,
    CreateAddressCommand PrimaryAddress);

