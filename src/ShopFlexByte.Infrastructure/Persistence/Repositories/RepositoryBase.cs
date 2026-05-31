using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopFlexByte.Infrastructure.Persistence.Repositories;

// K3: Low Coupling — centraliza as dependências de infraestrutura (IDbContextFactory e IMapper) em uma
//     base genérica reutilizável, evitando duplicação e reduzindo o acoplamento de cada repositório concreto.
// C1: Hierarquia extensível — UserRepository, ProductRepository, etc. herdam essa base parametrizada por DbContext.
public abstract class RepositoryBase<TDbContext>(IDbContextFactory<TDbContext> contextFactory, IMapper mapper) where TDbContext : DbContext
{
    protected IDbContextFactory<TDbContext> ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    protected IMapper Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
}
