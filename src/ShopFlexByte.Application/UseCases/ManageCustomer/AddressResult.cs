namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed record AddressResult(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string State,
    string ZipCode);
