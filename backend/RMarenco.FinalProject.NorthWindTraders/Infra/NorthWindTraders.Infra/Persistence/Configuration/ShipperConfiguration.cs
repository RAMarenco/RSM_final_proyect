using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable(nameof(Employee));

            builder.HasKey(e => e.ShipperID);

            builder.HasMany(e => e.Order)
                .WithOne()
                .HasForeignKey(o => o.ShipVia)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.CompanyName)
                .HasColumnType("nvarchar(40)")
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(e => e.Phone)
                .HasColumnType("nvarchar(24)")
                .HasMaxLength(24)
                .IsRequired();
        }
    }
}
