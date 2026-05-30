using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Application.Interfaces.UseCases;
using ShopFlexByte.Domain.Common;
using ShopFlexByte.Domain.ValueObjects;

namespace ShopFlexByte.Application.UseCases.ManageCustomer;

public sealed class UpdateCustomerUseCase(ICustomerRepository customerRepository) : IUpdateCustomerUserCase
{
    public async Task<Result> UpdateCustomerAsync(Guid customerId, UpdateCustomerCommand command)
    {
        if (command is null)
            return Result.Failure("Customer cannot be null.");

        var errors = new List<string>();

        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return Result.Failure("Customer not found.");
        }

        var fullNameResult = FullName.Create(command.FullName);

        if (fullNameResult.IsFailure)
            errors.Add(fullNameResult.Error);

        var emailResult = Email.Create(command.Email);

        if (emailResult.IsFailure)
            errors.Add(emailResult.Error);

        if (errors.Count > 0)
            return Result.Failure(string.Join(Environment.NewLine, errors));

        var updateCustomerResult = customer.Update(fullNameResult.Value, emailResult.Value);

        if (updateCustomerResult.IsFailure)
            return Result.Failure(updateCustomerResult.Error);

        await customerRepository.UpdateAsync(customer);

        return Result.Success();
    }
}
