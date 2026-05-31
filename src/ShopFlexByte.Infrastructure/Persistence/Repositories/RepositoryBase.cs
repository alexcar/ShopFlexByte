using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopFlexByte.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TDbContext>(IDbContextFactory<TDbContext> contextFactory, IMapper mapper) where TDbContext : DbContext
{
    protected IDbContextFactory<TDbContext> ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    protected IMapper Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
}
