using Microsoft.EntityFrameworkCore;
using ShopFlexByte.Infrastructure.Persistence.Entities;
using System.Reflection;

namespace ShopFlexByte.Infrastructure.Persistence.EntityFramework;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
