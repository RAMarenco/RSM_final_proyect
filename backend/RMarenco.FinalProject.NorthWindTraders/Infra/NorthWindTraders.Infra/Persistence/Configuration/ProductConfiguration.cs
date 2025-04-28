using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasKey(p => p.ProductID);

            builder.Property(e => e.ProductName)
               .HasColumnType("nvarchar(40)")
               .HasMaxLength(40)
               .IsRequired();
            builder.Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
