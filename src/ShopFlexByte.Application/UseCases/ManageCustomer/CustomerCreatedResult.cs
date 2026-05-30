namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed record CustomerCreatedResult(
    Guid Id,
    string FullName,
    string Email,
    AddressResult PrimaryAddress,
    IReadOnlyCollection<AddressResult> OtherAddresses);
