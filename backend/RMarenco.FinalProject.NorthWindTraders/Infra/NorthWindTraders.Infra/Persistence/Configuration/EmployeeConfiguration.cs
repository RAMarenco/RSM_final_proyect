using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Persistence.Configuration
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));

            builder.HasKey(e => e.EmployeeID);

            builder.HasMany(e => e.Order)
                .WithOne()
                .HasForeignKey(o => o.EmployeeID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.LastName)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.FirstName)
               .HasColumnType("nvarchar(10)")
               .HasMaxLength(10)
               .IsRequired();
            builder.Property(e => e.Photo)
               .HasColumnType("image")
               .IsRequired();
        }
    }
}
