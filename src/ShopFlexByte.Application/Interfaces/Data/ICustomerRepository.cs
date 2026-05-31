using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.Data;

// E2: Repository (DDD) — abstrai a persistência do aggregate Customer como se fosse uma coleção em memória,
//     expressando operações na linguagem do domínio (GetById, Create, Update, Delete).
// K3: Low Coupling — a interface vive na camada de Application e é implementada na Infrastructure, de modo
//     que os use cases dependem da abstração, não do EF Core (inversão de dependência / baixo acoplamento).
public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}
