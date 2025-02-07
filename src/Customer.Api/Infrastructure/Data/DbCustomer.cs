using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Infrastructure.Data;

public class DbCustomer : DbContext
{
    public DbCustomer(DbContextOptions<DbCustomer> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly);
    }

    public DbSet<Domain.Entities.Customer> Customers { get; set; }
}