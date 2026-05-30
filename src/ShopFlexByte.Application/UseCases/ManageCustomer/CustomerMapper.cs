using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Domain.ValueObjects;


namespace ShopFlexByte.Application.UseCases.ManageCustomer;

internal static class CustomerMapper
{
    internal static CustomerCreatedResult ToResult(Customer customer) =>
        new(
            Id: customer.Id,
            FullName: customer.FullName.ToString(),
            Email: customer.Email.Value,
            PrimaryAddress: ToAddressResult(customer.PrimaryAddress),
            OtherAddresses: customer.OtherAddresses
                .Select(ToAddressResult)
                .ToList()
                .AsReadOnly());

    internal static AddressResult ToAddressResult(Address address) =>
        new(
            Street: address.Street,
            Number: address.Number,
            Neighborhood: address.Neighborhood,
            City: address.City,
            State: address.State,
            ZipCode: address.ZipCode);
}
