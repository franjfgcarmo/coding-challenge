using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Api.Infrastructure.Data.Config;

public class CustomerConfig : IEntityTypeConfiguration<Domain.Entities.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .HasMaxLength(100);

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.PostalCode)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);
    }
}