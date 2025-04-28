using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            
            builder.HasKey(c => c.CustomerID);

            builder.HasMany(c => c.Order)
                .WithOne()
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.CompanyName)
                .HasColumnType("nvarchar(40)")
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}
