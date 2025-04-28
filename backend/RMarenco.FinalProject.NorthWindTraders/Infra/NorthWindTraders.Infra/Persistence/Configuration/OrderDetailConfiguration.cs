using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new { od.OrderID, od.ProductID });

            builder.HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderID)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");
            builder.Property(od => od.Quantity)
                .IsRequired();
        }
    }
}
