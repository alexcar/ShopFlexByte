using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopFlexByte.Application.Interfaces.Data;
using ShopFlexByte.Domain.Entities;
using ShopFlexByte.Infrastructure.Persistence.EntityFramework;

namespace ShopFlexByte.Infrastructure.Persistence.Repositories;

public class UserRepository(IDbContextFactory<CoreDbContext> contextFactory, IMapper mapper) : RepositoryBase<CoreDbContext>(contextFactory, mapper), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var dbContext = await ContextFactory.CreateDbContextAsync();
        var sqlUser = await dbContext.Users.FindAsync(userId);
        return Mapper.Map<User>(sqlUser);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var dbContext = await ContextFactory.CreateDbContextAsync();
        var sqlUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return Mapper.Map<User>(sqlUser);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var dbContext = await ContextFactory.CreateDbContextAsync();
        var sqlUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return Mapper.Map<User>(sqlUser);
    }

    public async Task CreateUserAsync(User user)
    {
        var sqlUser = Mapper.Map<Entities.User>(user);
        var dbContext = await ContextFactory.CreateDbContextAsync();
        await dbContext.Users.AddAsync(sqlUser);
        await dbContext.SaveChangesAsync();
    }
}
