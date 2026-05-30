namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed record CreateAddressCommand(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string State,
    string ZipCode);
