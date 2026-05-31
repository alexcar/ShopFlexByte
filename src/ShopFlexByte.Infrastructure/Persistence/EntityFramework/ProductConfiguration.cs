using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlexByte.Infrastructure.Persistence.Entities;

namespace ShopFlexByte.Infrastructure.Persistence.EntityFramework;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).ValueGeneratedNever();
        builder.Property(product => product.Name).IsRequired().HasMaxLength(255);
        builder.Property(product => product.Price).IsRequired().HasPrecision(18, 2);
        builder.Property(product => product.StockLevel).IsRequired();
    }
}
