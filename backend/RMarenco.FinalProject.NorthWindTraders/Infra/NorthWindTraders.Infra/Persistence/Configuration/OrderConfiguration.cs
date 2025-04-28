using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.OrderID);

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeID)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Shipper)
                .WithMany()
                .HasForeignKey(s => s.ShipVia)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(o => o.OrderID)
                .ValueGeneratedOnAdd();
            builder.Property(o => o.CustomerID)
                .HasColumnType("nvarchar(5)")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(o => o.EmployeeID)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(o => o.OrderDate)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(o => o.ShipVia)
                .HasColumnType("int");
            builder.Property(o => o.ShipAddress)
                .HasColumnType("nvarchar(60)")
                .HasMaxLength(60);
            builder.Property(o => o.ShipCity)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15);
            builder.Property(o => o.ShipRegion)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15);
            builder.Property(o => o.ShipPostalCode)
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);
            builder.Property(o => o.ShipCountry)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15);
        }
    }
}
