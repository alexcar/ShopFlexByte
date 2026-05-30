using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.Data;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}
