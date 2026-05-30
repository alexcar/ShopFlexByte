using ShopFlexByte.Application.UseCases.ManageCustomer;

namespace ShopFlexByte.Application.UseCases.AddCustomer;

public record CustomerDto()
{
    public string FullName { get; init; } = default!;
    public string Cpf { get; init; } = default!;
    public string Email { get; init; } = default!;
    public AddressDto PrimaryAddress { get; init; } = default!;
    public List<AddressDto> OtherAddresses { get; set; } = default!;
}
