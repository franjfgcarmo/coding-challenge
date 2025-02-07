using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Customer.Api.Infrastructure.Data.SeedWord;

public class DatabaseSeeder(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbCustomer>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await SeedData(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task SeedData(DbCustomer dbContext)
    {
        if (!await dbContext.Customers.AnyAsync())
        {
            await dbContext.Customers.AddRangeAsync(CustomerSeeds.DefaultCustomers());
            await dbContext.SaveChangesAsync();
        }
    }
}