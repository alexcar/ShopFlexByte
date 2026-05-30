using ShopFlexByte.Domain.Entities;

namespace ShopFlexByte.Application.Interfaces.Data;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task CreateUserAsync(User user);
}
